package br.com.listadecompras.listadecompras;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Environment;
import android.os.Handler;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.sql.SQLException;

import br.com.listadecompras.listadecompras.dao.DataBaseHelper;

public class SplashActivity extends AppCompatActivity {
    private static int SPLASH_TIME_OUT = 3000;
    private static String DB_PATH = "/data/data/br.com.listadecompras.listadecompras/databases/";

    private static String DB_NAME = "listadecompras";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);

        try {
            GerenciaDB();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        if(ActivityCompat.checkSelfPermission(this, Manifest.permission.INTERNET)
                != PackageManager.PERMISSION_GRANTED){
            ActivityCompat.requestPermissions(this,  new String[]{Manifest.permission.INTERNET}, 123);
            finish();

        }else if(ActivityCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE)
                != PackageManager.PERMISSION_GRANTED){

            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE}, 123);
            finish();

        }else{
            new Handler().postDelayed(new Runnable() {
                @Override
                public void run() {
                    SalvaBancoDeDados();
                    Intent i = new Intent(SplashActivity.this, ListaCompraActivity.class);
                    startActivity(i);
                    finish();
                }
            }, SPLASH_TIME_OUT);
        }
    }

    private void GerenciaDB() throws SQLException {
        DataBaseHelper myDbHelper = new DataBaseHelper(this);

        try {

            myDbHelper.createDataBase();

        } catch (IOException ioe) {

            throw new Error("Não foi possivel criar o banco de dados.");
        }

        try {

            myDbHelper.openDataBase();

        }catch(SQLException sqle){

            throw sqle;

        }finally {
            myDbHelper.close();
        }
    }

  private void SalvaBancoDeDados(){
      try {
          // Caminho de Origem do Seu Banco de Dados
          InputStream in = new FileInputStream(
                  new File(DB_PATH + DB_NAME));

          // Caminho de Destino do Backup do Seu Banco de Dados
          OutputStream out = new FileOutputStream(new File(
                  Environment.getExternalStorageDirectory()
                          + "/listadecompras"));

          byte[] buf = new byte[1024];
          int len;
          while ((len = in.read(buf)) > 0) {
              out.write(buf, 0, len);
          }
          in.close();
          out.close();
      } catch (FileNotFoundException e) {
          e.printStackTrace();
      } catch (IOException e) {
          e.printStackTrace();
      }
  }
}
