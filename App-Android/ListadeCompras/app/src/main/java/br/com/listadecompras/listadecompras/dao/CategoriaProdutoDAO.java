package br.com.listadecompras.listadecompras.dao;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;

import br.com.listadecompras.listadecompras.modelo.CategoriaProduto;

/**
 * Created by Julio on 05/11/2017.
 */

public class CategoriaProdutoDAO {
    private static final String TABELA = "CategoriaProduto";
    private Conexao conexao;
    private Context context;

    public CategoriaProdutoDAO(Context ctx){
        conexao = new Conexao(ctx);
        context = ctx;
    }

    public void close(){
        conexao.close();
    }

    public ArrayList<CategoriaProduto> FindAll(){
        String sql = "  SELECT * FROM "+ TABELA + ";";
        SQLiteDatabase db = conexao.getWritableDatabase();
        Cursor c = db.rawQuery(sql, null);

        ArrayList<CategoriaProduto> CategoriasDosProdutos = new ArrayList<>();

        while (c.moveToNext()){
            CategoriaProduto categoriaProduto = new CategoriaProduto();

            categoriaProduto.setIdCategoriaProduto(c.getInt(c.getColumnIndex("IdCategoriaProduto")));
            categoriaProduto.setNome(c.getString(c.getColumnIndex("Nome")));

            CategoriasDosProdutos.add(categoriaProduto);
        }
        return CategoriasDosProdutos;
    }
}
