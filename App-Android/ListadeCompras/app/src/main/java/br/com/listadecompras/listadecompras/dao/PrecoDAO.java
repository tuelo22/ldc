package br.com.listadecompras.listadecompras.dao;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;
import br.com.listadecompras.listadecompras.modelo.Preco;
import br.com.listadecompras.listadecompras.modelo.Produto;


/**
 * Created by Julio on 21/04/2017.
 */

public class PrecoDAO {
    private static final String Preco = "Preco";
    Conexao conexao;

    public PrecoDAO(Context ctx){
        conexao = new Conexao(ctx);
    }

    public Preco FindbyId(Integer IdPreco){
        String sql = "  SELECT * FROM "+ Preco + " WHERE IdPreco = " + IdPreco + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()){
            return RecuperaPreco(c);
        }

        return null;
    }

    private Preco RecuperaPreco(Cursor c) {
        Preco preco = new Preco();

        preco.setIdPreco(c.getInt(c.getColumnIndex("IdPreco")));
        preco.setIdProdutoFK(c.getInt(c.getColumnIndex("IdProdutoFK")));
        preco.setPreco(c.getDouble(c.getColumnIndex("Preco")));
        preco.setMercado(c.getString(c.getColumnIndex("Mercado")));
        preco.setObservacao(c.getString(c.getColumnIndex("Observacao")));
        preco.setIdListaFK(c.getInt(c.getColumnIndex("IdListaFK")));

        return preco;
    }

    public ArrayList<Preco> FindbyIdProduto(Integer IdProduto, Integer IdLista){

        String sql = "  SELECT * FROM "+ Preco + " WHERE IdProdutoFK = " + IdProduto
                                               + "   AND IdListaFK   = " + IdLista + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Preco> precos = new ArrayList<>();

        while (c.moveToNext()){
            precos.add(RecuperaPreco(c));
        }

        return precos;
    }

    public ArrayList<Preco> FindAll(){

        String sql = "  SELECT * FROM "+ Preco + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Preco> precos = new ArrayList<>();

        while (c.moveToNext()){
            precos.add(RecuperaPreco(c));
        }

        return precos;
    }

    public void Insert(Preco preco){
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("IdPreco", preco.getIdPreco());
        cv.put("Preco", preco.getPreco());
        cv.put("Mercado", preco.getMercado());
        cv.put("Observacao", preco.getObservacao());
        cv.put("IdProdutoFK", preco.getIdProdutoFK());
        cv.put("IdListaFK", preco.getIdListaFK());

        db.insert(Preco, null, cv);
    }

    public void Delete(Integer IdPreco){
        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(IdPreco)};
        db.delete(Preco, "IdPreco = ?", params);
    }

    public void DeleteByIdProduto(Integer IdProdutoFK, Integer IdListaFK){
        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(IdProdutoFK),Integer.toString(IdListaFK)};
        db.delete(Preco, "IdProdutoFK = ? and IdListaFK = ?", params);
    }

    public void DeleteByIdLista(Integer IdListaFK){
        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(IdListaFK)};
        db.delete(Preco, "IdListaFK = ?", params);
    }

    public void Update(Preco preco){
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("IdPreco", preco.getIdPreco());
        cv.put("Preco", preco.getPreco());
        cv.put("Mercado", preco.getMercado());
        cv.put("Observacao", preco.getObservacao());
        cv.put("IdProdutoFK", preco.getIdProdutoFK());
        cv.put("IdListaFK", preco.getIdListaFK());

        String[] params = {Integer.toString(preco.getIdPreco())};

        db.update(Preco, cv,"IdPreco = ?", params);
    }

    public void close(){
        conexao.close();
    }

    public Preco MenorValor(Integer IdProduto, Integer IdLista){
        String sql = "  SELECT * "
                   + "    FROM   " + Preco
                   + "   WHERE IdProdutoFK = " + IdProduto
                   + "     AND IdListaFK   = " + IdLista
                   + "     AND Preco IN (SELECT MIN(pc.Preco) "
                   + "                    FROM " + Preco + " pc "
                   + "                   WHERE IdProdutoFK = " + IdProduto
                   + "                     AND IdListaFK   = " + IdLista + ");";

        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()){
            return RecuperaPreco(c);
        }

        return null;
    }
}
