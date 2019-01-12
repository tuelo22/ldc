package br.com.listadecompras.listadecompras.dao;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.support.annotation.NonNull;
import android.widget.TabHost;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;
import br.com.listadecompras.listadecompras.modelo.StatusPrdEmLista;

/**
 * Created by Julio on 29/01/2017.
 */

public class ListaDAO {
    private static final String Lista = "Lista";
    private static final String ListaProduto = "ListaProduto";
    private Conexao conexao;
    private Context context;

    public ListaDAO(Context ctx){
        conexao = new Conexao(ctx);
        context = ctx;
    }

    public void close(){
        conexao.close();
    }

    public void Insere(Lista lista){
        ContentValues cv = new ContentValues();

        cv.put("Nome", lista.getNome());
        cv.put("DataCadastro" ,lista.getDataCadastro());

        conexao.getWritableDatabase().insert(Lista, null, cv);

        lista.setIdLista(ConsultaID());

        if(lista.getProdutos() != null){
            RelacionaListaProduto(lista);
        }
    }

    public void Update(Lista lista){
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("Nome", lista.getNome());

        String[] params = {Integer.toString(lista.getIdLista())};

        db.update(Lista, cv,"IdLista = ?", params);
    }

    public void Delete(Integer IdLista){
        PrecoDAO pcd = new PrecoDAO(context);
        pcd.DeleteByIdLista(IdLista);
        pcd.close();

        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(IdLista)};
        db.delete(ListaProduto, "IdListaFK = ?", params);
        db.delete(Lista, "IdLista = ?", params);
    }

    public ArrayList<Lista> FindAll() {
        String sql = "  SELECT * FROM " + Lista + " ORDER BY DataCadastro;";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Lista> listas = new ArrayList<Lista>();

        while (c.moveToNext()) {

          Lista lista = RecuperaListas(c, false);

          listas.add(lista);
        }

      return  listas;
    }

    public Lista FindById(Integer IdLista) {
        String sql = "  SELECT * FROM "+ Lista + "WHERE IdLista =" + IdLista + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()) {
            Lista lista = RecuperaListas(c, true);
            return  lista;
        }
        return null;
    }

    @NonNull
    private Lista RecuperaListas(Cursor c, Boolean ConsultaProdutos) {

            Lista lista = new Lista();

            lista.setIdLista(c.getInt(c.getColumnIndex("IdLista")));
            lista.setNome(c.getString(c.getColumnIndex("Nome")));
            lista.setDataCadastro(c.getString(c.getColumnIndex("DataCadastro")));

            if(ConsultaProdutos){
                ProdutoDAO pd = new ProdutoDAO(context);
                lista.setProdutos(pd.FindByIdLista(lista.getIdLista()));
                pd.close();
            }

        return lista;
    }

    public void RelacionaListaProduto(Lista lista){

        ContentValues cv = new ContentValues();

        for (Produto produto :lista.getProdutos()) {
            cv.put("IdListaFK", lista.getIdLista());
            cv.put("IdProdutoFK", produto.getIdProduto());

            conexao.getWritableDatabase().insert(ListaProduto, null, cv);

            cv.clear();
        }
    }

    public void RelacionaListaProduto(Integer Idlista, ArrayList<Produto> produtos){

        ContentValues cv = new ContentValues();

        for (Produto produto : produtos) {
            cv.put("IdListaFK", Idlista);
            cv.put("IdProdutoFK", produto.getIdProduto());

            conexao.getWritableDatabase().insert(ListaProduto, null, cv);

            cv.clear();
        }
    }

    public String FindStatusProduto(Integer IdLista, Integer IdProduto){
        String sql = "  SELECT StatusPrdEmLista FROM " + ListaProduto + " WHERE IdListaFK = " + IdLista + "   AND IdProdutoFK = "+ IdProduto +" ;";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()) {

             String status = c.getString(c.getColumnIndex("StatusPrdEmLista"));

            if (status != null){
                return status;
            }
        }
        return StatusPrdEmLista.Pendente.name();
    }

    public String FindTotalLista(Integer IdLista, Integer IdProduto){
        String sql = "  SELECT StatusPrdEmLista FROM " + ListaProduto + " WHERE IdListaFK = " + IdLista + "   AND IdProdutoFK = "+ IdProduto +" ;";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()) {

            String status = c.getString(c.getColumnIndex("StatusPrdEmLista"));

            if (status != null){
                return status;
            }
        }
        return StatusPrdEmLista.Pendente.name();
    }

    public BigDecimal FindQuantidade(Integer IdLista, Integer IdProduto) {
        String sql = "  SELECT Quantidade FROM " + ListaProduto + " WHERE IdListaFK = " + IdLista + "   AND IdProdutoFK = " + IdProduto + " ;";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()) {
            String quantidade = c.getString(c.getColumnIndex("Quantidade"));

            if( quantidade != null && ! quantidade.isEmpty()){
                return new BigDecimal(quantidade);
            }else{
                return new BigDecimal(0.0);
            }
        }

        return new BigDecimal(0.0);
    }

    public void UpdateStatus (Integer IdLista, Integer IdProduto, String StatusPrdEmLista){
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("StatusPrdEmLista", StatusPrdEmLista);

        String[] params = {Integer.toString(IdLista),Integer.toString(IdProduto)};

        db.update(ListaProduto, cv,"IdListaFK = ? and IdProdutoFK = ?", params);

    }

    public void UpdateQuantidade (Integer IdLista, Integer IdProduto, BigDecimal quantidade){
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("Quantidade", quantidade.toString());

        String[] params = {Integer.toString(IdLista),Integer.toString(IdProduto)};

        db.update(ListaProduto, cv,"IdListaFK = ? and IdProdutoFK = ?", params);

    }

    private int ConsultaID(){
        String sql = "SELECT MAX(IdLista) IdLista FROM "+ Lista + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if(c.moveToNext()){
            return c.getInt(c.getColumnIndex("IdLista"));
        }
        return 0;
    }

    public void DeleteProdutoLista(Integer IdLista, Integer IdProduto){
        PrecoDAO pd = new PrecoDAO(context);
        pd.DeleteByIdProduto(IdProduto, IdLista);
        pd.close();

        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(IdProduto),Integer.toString(IdLista)};
        db.delete(ListaProduto, "IdProdutoFK = ? and IdListaFK = ?", params);
    }

    public BigDecimal FindValorTotalLista(Integer IdLista){
        String sql = " SELECT round(SUM(QUANTIDADE*MENORPRECO),2) VALOR";
         sql = sql + "   FROM (SELECT LP.IdProdutoFK , LP.IdListaFK Lista, LP.Quantidade QUANTIDADE, MIn(P.Preco) MENORPRECO";
         sql = sql + "           FROM LISTA L , LISTAPRODUTO LP, PRECO P ";
         sql = sql + "          WHERE L.IdLista = LP.IdListaFK ";
         sql = sql + "            AND LP.IdListaFK = P.IdListaFK ";
         sql = sql + "            AND LP.IdProdutoFK = P.IdProdutoFK ";
         sql = sql + "          GROUP BY LP.IdProdutoFK , LP.IdListaFK )";
         sql = sql + "  WHERE Lista = " + IdLista;

        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()) {
            String valor = c.getString(c.getColumnIndex("VALOR"));

            if( valor != null && ! valor.isEmpty()){
                return new BigDecimal(valor);
            }else{
                return new BigDecimal(0.0);
            }
        }

        return new BigDecimal(0.0);
    }

}
