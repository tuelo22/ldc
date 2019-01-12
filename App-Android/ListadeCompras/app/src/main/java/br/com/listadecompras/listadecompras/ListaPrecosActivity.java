package br.com.listadecompras.listadecompras;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
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

import java.util.ArrayList;

import br.com.listadecompras.listadecompras.dao.PrecoDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Preco;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class ListaPrecosActivity extends AppCompatActivity {
    private ListView LvPrecos;
    private Produto produto;
    private Integer IdLista;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lista_precos);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        LvPrecos = (ListView) findViewById(R.id.lvcadastropreco);
        registerForContextMenu(LvPrecos);

        Intent it = getIntent();
        produto = (Produto) it.getSerializableExtra("produto");
        IdLista = (Integer) it.getSerializableExtra("idlista");

        if(produto != null){
            this.setTitle("Preços: " + produto.getNome());
        };

        CarregaListView();

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(ListaPrecosActivity.this, CadastroPrecoActivity.class);
                intent.putExtra("produto", produto);
                intent.putExtra("idlista", IdLista);
                startActivity(intent);
            }
        });

        LvPrecos.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> lista, View item, int position, long id) {
                Preco preco = (Preco) lista.getItemAtPosition(position);
                Intent intent = new Intent(ListaPrecosActivity.this, CadastroPrecoActivity.class);
                intent.putExtra("preco", preco);
                intent.putExtra("produto", produto);
                intent.putExtra("idlista", IdLista);
                startActivity(intent);
            }
        });
    }

    private void CarregaListView() {
        final ArrayList<Preco> precos;
        PrecoDAO pc = new PrecoDAO(this);
        precos = pc.FindbyIdProduto(produto.getIdProduto(), IdLista);
        pc.close();

        ArrayAdapter<Preco> adapter = new ArrayAdapter<Preco>(this, android.R.layout.simple_list_item_2, android.R.id.text1, precos){
            @Override
            public View getView(int position, View convertView, ViewGroup parent) {
                View view = super.getView(position, convertView, parent);
                TextView text1 = (TextView) view.findViewById(android.R.id.text1);
                TextView text2 = (TextView) view.findViewById(android.R.id.text2);

                text1.setText(precos.get(position).getMercado());
                text2.setText(precos.get(position).getPrecoString());

                return view;
            }
        };
        LvPrecos.setAdapter(adapter);
    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v, final ContextMenu.ContextMenuInfo menuInfo) {
        MenuItem deletar = menu.add("Deletar");
        deletar.setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                Preco preco = (Preco) LvPrecos.getItemAtPosition(info.position);
                PrecoDAO dao = new PrecoDAO(ListaPrecosActivity.this);
                dao.Delete(preco.getIdPreco());
                dao.close();
                CarregaListView();

                return false;
            }
        });

        super.onCreateContextMenu(menu, v, menuInfo);
    }

    @Override
    protected void onResume() {
        CarregaListView();

        super.onResume();
    }
}
