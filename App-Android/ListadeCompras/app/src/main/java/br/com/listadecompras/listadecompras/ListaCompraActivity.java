package br.com.listadecompras.listadecompras;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.ContextMenu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;
import java.util.ArrayList;

import br.com.listadecompras.listadecompras.Componentes.ATLista;
import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;

public class ListaCompraActivity extends AppCompatActivity {
    private ListView Lvlistas;
    private AdView adView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lista_compra);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        Lvlistas = (ListView) findViewById(R.id.Listagem_compra);

        adView = (AdView) findViewById(R.id.adBanner);
        AdRequest adRequest = new AdRequest.Builder().build();
        adView.loadAd(adRequest);

        registerForContextMenu(Lvlistas);

        CarregaListView();

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent CadastroLista = new Intent(ListaCompraActivity.this, CadastroListaActivity.class);
                startActivity(CadastroLista);
            }
        });

        Lvlistas.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int posicao, long id) {
                Lista lista = (Lista) adapterView.getItemAtPosition(posicao);
                Intent i = new Intent(ListaCompraActivity.this, ListaDeComprasActivity.class);
                i.putExtra("lista", lista);
                startActivity(i);
            }
        });

    }

    @Override
    public void onResume() {
        super.onResume();

        CarregaListView();

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

    private void CarregaListView() {
        final ArrayList<Lista> listas;
        ListaDAO ld = new ListaDAO(this);
        listas = ld.FindAll();
        ld.close();


        ATLista adapter =  new ATLista(listas, this);

     /*   ArrayAdapter<Lista> adapter = new ArrayAdapter<Lista>(
                this,
                android.R.layout.simple_list_item_2,
                android.R.id.text1,
                listas){
            @Override
            public View getView(int position, View convertView, ViewGroup parent) {
                View view = super.getView(position, convertView, parent);
                TextView text1 = (TextView) view.findViewById(android.R.id.text1);
                TextView text2 = (TextView) view.findViewById(android.R.id.text2);

                text1.setText(listas.get(position).getNome());
                text2.setText(listas.get(position).getDataCadastro());

                return view;
            }
        };*/

        Lvlistas.setAdapter(adapter);
    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, final View v, final ContextMenu.ContextMenuInfo menuInfo) {
        MenuItem deletar = menu.add("Deletar");
        deletar.setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                final Lista lista = (Lista) Lvlistas.getItemAtPosition(info.position);

                new AlertDialog.Builder(ListaCompraActivity.this)
                        .setMessage("Deseja deletar " + lista.getNome() + "?")
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                                    @Override
                                    public void onClick(DialogInterface dialog, int which) {
                                       ListaDAO ld = new ListaDAO(ListaCompraActivity.this);
                                       ld.Delete(lista.getIdLista());
                                       ld.close();
                                       CarregaListView();
                                       Snackbar.make(v, lista.getNome() + " deletada !", Snackbar.LENGTH_LONG)
                                                .setAction("Action", null).show();

                                    }
                                }
                        )
                        .setNegativeButton(android.R.string.no, null).show();
                return false;
            }
        });

        MenuItem renomear = menu.add("Renomear");
        renomear.setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                final Lista lista = (Lista) Lvlistas.getItemAtPosition(info.position);

                Intent intent = new Intent(ListaCompraActivity.this, CadastroListaActivity.class);
                intent.putExtra("lista", lista);
                startActivity(intent);

               return false;
            }
        });

    }
}
