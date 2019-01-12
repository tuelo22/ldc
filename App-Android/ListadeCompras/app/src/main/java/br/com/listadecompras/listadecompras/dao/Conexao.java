package br.com.listadecompras.listadecompras.dao;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by Julio on 26/10/2016.
 */

public class Conexao extends SQLiteOpenHelper {
    protected static final String DATABASE ="listadecompras";
    protected static final int VERSAO = 1;
    protected Context contexto;

    public Conexao(Context ctx){
        super(ctx, Conexao.DATABASE, null, Conexao.VERSAO);
        contexto = ctx;
    }

    @Override
    public void onCreate(SQLiteDatabase Database){
    }

    @Override
    public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {

    }

}
