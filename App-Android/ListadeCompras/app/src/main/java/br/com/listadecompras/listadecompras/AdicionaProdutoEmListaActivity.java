package br.com.listadecompras.listadecompras;

import android.content.Intent;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.SparseBooleanArray;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;

import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class AdicionaProdutoEmListaActivity extends AppCompatActivity {
    private ListView LvProdutos;
    private Lista lista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_adiciona_produto_em_lista);

        LvProdutos = (ListView) findViewById(R.id.apellistageprodutos);

        Intent it = getIntent();

        lista = (Lista) it.getSerializableExtra("lista");

        if(lista != null){
            this.setTitle(lista.getNome());
        }

        CarregaListView();
    }

    private void CarregaListView() {
        ArrayList<Produto> produtos;
        ProdutoDAO pd = new ProdutoDAO(this);
        produtos = pd.FindByOutIdLista(lista.getIdLista());
        pd.close();

        LvProdutos.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        ArrayAdapter<Produto> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_multiple_choice, produtos);
        LvProdutos.setAdapter(adapter);
    }

    @Override
    protected void onResume() {

        CarregaListView();

        super.onResume();
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

                lista.setProdutos(retornaProdutosSelecionados());

                ListaDAO ld = new ListaDAO(this);
                ld.RelacionaListaProduto(lista);
                ld.close();
                Toast.makeText(this, "Produtos adicionados.", Toast.LENGTH_SHORT).show();
                finish();

                break;
        }

        return super.onOptionsItemSelected(item);
    }


    protected ArrayList<Produto> retornaProdutosSelecionados(){
        SparseBooleanArray clickedItemPositions = LvProdutos.getCheckedItemPositions();

        ArrayList<Produto> produtos = new ArrayList<Produto>();

        for(int index=0;index<clickedItemPositions.size();index++){

            boolean checked = clickedItemPositions.valueAt(index);

            if(checked){
                int key = clickedItemPositions.keyAt(index);
                Produto produto = (Produto) LvProdutos.getItemAtPosition(key);

                produtos.add(produto);
            }
        }
        return produtos;
    }

}
