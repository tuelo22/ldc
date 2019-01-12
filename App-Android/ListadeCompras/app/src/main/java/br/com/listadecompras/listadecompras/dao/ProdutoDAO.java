package br.com.listadecompras.listadecompras.dao;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.widget.ArrayAdapter;

import java.util.ArrayList;
import java.util.List;

import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;

/**
 * Created by Julio on 03/02/2017.
 */

public class ProdutoDAO {
    private static final String Produto = "Produto";
    private static final String ListaProduto = "ListaProduto";
    Conexao conexao;

    public ProdutoDAO(Context ctx){
        conexao = new Conexao(ctx);
    }

    public ArrayList<Produto> FindAll(){
        String sql = "  SELECT * FROM "+ Produto + " ORDER BY Nome; ";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Produto> produtos = new ArrayList<>();

        while (c.moveToNext()){
            produtos.add(RecuperaProduto(c));
        }
        return produtos;
    }

    public ArrayList<Produto> FindByIdLista(int IdLista){
        String sql = "  SELECT * FROM "+ Produto
                   + "     INNER JOIN "+ ListaProduto
                   + "     ON IdProduto = IdProdutoFK WHERE IdListaFK = " + IdLista + " ORDER BY Nome; ";

        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Produto> produtos = new ArrayList<>();

        while (c.moveToNext()){
            produtos.add(RecuperaProduto(c));
        }
        return produtos;
    }

    public ArrayList<Produto> FindByOutIdLista(int IdLista){
        String sql = "  SELECT * FROM "+ Produto
                + "      WHERE NOT EXISTS ( SELECT * FROM " + ListaProduto
                + "                          WHERE IdProduto = IdProdutoFK AND IdListaFK = " + IdLista + ") ORDER BY Nome; ";

        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Produto> produtos = new ArrayList<>();

        while (c.moveToNext()){
            produtos.add(RecuperaProduto(c));
        }
        return produtos;
    }

    public ArrayList<Produto> FindByOutIdLista(int IdLista, int idCategoriaProdutoFK){
        String sql = "  SELECT * FROM "+ Produto
                + "      WHERE IdCategoriaProdutoFK = " + idCategoriaProdutoFK
                + "        AND NOT EXISTS ( SELECT * FROM " + ListaProduto
                + "                          WHERE IdProduto = IdProdutoFK AND IdListaFK = " + IdLista + ") ORDER BY Nome; ";

        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Produto> produtos = new ArrayList<>();

        while (c.moveToNext()){
            produtos.add(RecuperaProduto(c));
        }
        return produtos;
    }

    public ArrayList<Produto> FindByIdCategoriaProduto(int idCategoriaProdutoFK){
        String sql = "  SELECT * FROM "+ Produto + " WHERE IdCategoriaProdutoFK = " + idCategoriaProdutoFK + " ORDER BY Nome;";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<Produto> produtos = new ArrayList<>();

        while (c.moveToNext()){
            produtos.add(RecuperaProduto(c));
        }
        return produtos;
    }

    public Produto FindById(int IdProduto){
        String sql = "  SELECT * FROM "+ Produto + " WHERE IdProduto = " + IdProduto + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if (c.moveToNext()){
            return RecuperaProduto(c);
        }

        return null;
    }

    private Produto RecuperaProduto(Cursor c) {
        Produto produto = new Produto();

        produto.setIdProduto(c.getInt(c.getColumnIndex("IdProduto")));
        produto.setNome(c.getString(c.getColumnIndex("Nome")));
        produto.setIdCategoriaProdutoFK(c.getInt(c.getColumnIndex("IdCategoriaProdutoFK")));

        return  produto;
    }

    private int ConsultaID(){
        String sql = "SELECT MAX(IdProduto) IdProduto FROM "+ Produto + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        if(c.moveToNext()){
            return c.getInt(c.getColumnIndex("IdProduto"));
        }
        return 0;
    }

    public void Insere(Produto produto){

        ContentValues cv = new ContentValues();

        cv.put("Nome", produto.getNome());
        cv.put("IdCategoriaProdutoFK", produto.getIdCategoriaProdutoFK());

        conexao.getWritableDatabase().insert(Produto, null, cv);

        produto.setIdProduto(ConsultaID());
    }

    public void Delete(Produto produto) {
        SQLiteDatabase db = conexao.getWritableDatabase();
        String[] params = {Integer.toString(produto.getIdProduto())};
        db.delete(Produto, "IdProduto = ?", params);
    }

    public void DeleteRelProdutoLista(Produto produto, Lista lista) {
        SQLiteDatabase db = conexao.getWritableDatabase();

        String[] params = {Integer.toString(produto.getIdProduto()),
                           Integer.toString(lista.getIdLista())};

        db.delete(ListaProduto, "IdProdutoFK = ? and IdListaFK = ?", params);
    }

    public void update(Produto produto) {
        SQLiteDatabase db = conexao.getWritableDatabase();
        ContentValues cv = new ContentValues();

        cv.put("Nome", produto.getNome());

        String[] params = {Integer.toString(produto.getIdProduto())};

        db.update(Produto, cv,"IdProduto = ?", params);

    }

    public void close(){
        conexao.close();
    }
}
