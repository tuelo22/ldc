package br.com.listadecompras.listadecompras;

import android.content.Context;
import android.content.Intent;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.RecyclerView;
import android.util.SparseBooleanArray;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Adapter;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;

import java.sql.Time;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class CadastroListaActivity extends AppCompatActivity {
    private EditText edtnomelista;
    private AdView adView;
    private Lista lista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cadastro_lista);
        edtnomelista = (EditText) findViewById(R.id.edtnomelista);

        Intent it = getIntent();
        lista = (Lista) it.getSerializableExtra("lista");

        if(lista != null){
            edtnomelista.setText(lista.getNome());
        }

        adView = (AdView) findViewById(R.id.adBanner);
        AdRequest adRequest = new AdRequest.Builder().build();
        adView.loadAd(adRequest);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_cadastro_lista, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.menu_formulario_ok:

                if (lista != null){

                    lista.setNome(edtnomelista.getText().toString());

                    ListaDAO ld = new ListaDAO(this);
                    ld.Update(lista);
                    ld.close();

                    Toast.makeText(CadastroListaActivity.this, "Lista Atualizada !", Toast.LENGTH_SHORT).show();
                } else {

                    Lista lista = new Lista();

                    long date = System.currentTimeMillis();
                    SimpleDateFormat sdf = new SimpleDateFormat("d/M/yyyy");

                    lista.setNome(edtnomelista.getText().toString());
                    lista.setDataCadastro(sdf.format(date).toString());

                    ListaDAO ld = new ListaDAO(this);
                    ld.Insere(lista);
                    ld.close();

                    Toast.makeText(CadastroListaActivity.this, "Lista cadastrada !", Toast.LENGTH_SHORT).show();
                }

                finish();

                break;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onResume() {
        super.onResume();

        if(adView != null){
            adView.resume();
        }
    }

    @Override
    protected void onPause() {
        if(adView != null){
            adView.pause();
        }
        super.onPause();
    }

    @Override
    protected void onDestroy() {
        if(adView != null){
            adView.destroy();
        }
        super.onDestroy();
    }
}
