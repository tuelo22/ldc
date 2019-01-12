package br.com.listadecompras.listadecompras;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.SparseBooleanArray;
import android.view.ContextMenu;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import java.util.ArrayList;

import br.com.listadecompras.listadecompras.dao.CategoriaProdutoDAO;
import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.CategoriaProduto;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class SelecaoDeProdutoActivity extends AppCompatActivity {
    private Spinner spnCategoria;
    private ListView LvProdutos;
    private ArrayList<CategoriaProduto> categorias;
    private CategoriaProduto CategoriaAnterior;
    private Lista lista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_selecao_de_produto);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        spnCategoria = (Spinner) findViewById(R.id.spnCategoria);
        LvProdutos = (ListView) findViewById(R.id.lvprodutos);

        registerForContextMenu(LvProdutos);

        Intent i = getIntent();

        lista = (Lista) i.getSerializableExtra("lista");

        if(lista != null){
            this.setTitle(lista.getNome());
        }

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent CadastroProduto = new Intent(SelecaoDeProdutoActivity.this, CadastroProdutoActivity.class);

                CategoriaProduto categoria = (CategoriaProduto) spnCategoria.getSelectedItem();

                CadastroProduto.putExtra("categoria", categoria);

                startActivity(CadastroProduto);
            }
        });

        MontaTelaInicial();
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_cadastro_lista, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        CategoriaAnterior.setProdutos(retornaProdutosSelecionados());

        switch (item.getItemId()) {
            case R.id.menu_formulario_ok:

                if(categorias != null){
                    for (CategoriaProduto cat : categorias) {
                        if(cat.getProdutos() != null){
                            ListaDAO ld = new ListaDAO(this);
                            ld.RelacionaListaProduto(lista.getIdLista(), cat.getProdutos());
                            ld.close();
                        }
                    }
                }

                Toast.makeText(SelecaoDeProdutoActivity.this, "Produto cadastrado !", Toast.LENGTH_SHORT).show();
                finish();
                break;
        }
        return super.onOptionsItemSelected(item);
    }

    private void MontaTelaInicial(){
        CategoriaProdutoDAO cd = new CategoriaProdutoDAO(this);
        categorias =  cd.FindAll();
        cd.close();

        CategoriaAnterior = categorias.get(0);

        ArrayAdapter<CategoriaProduto> CatPadapter = new ArrayAdapter<CategoriaProduto>(this,
                android.R.layout.simple_spinner_dropdown_item,
                categorias);

        CatPadapter.setDropDownViewResource( android.R.layout.simple_spinner_dropdown_item);

        spnCategoria.setAdapter(CatPadapter);

        spnCategoria.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

                CategoriaAnterior.setProdutos(retornaProdutosSelecionados());

                CategoriaProduto categoria = (CategoriaProduto) parent.getItemAtPosition(position);

                CarregaListView(categoria);

                CategoriaAnterior = categoria;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        ProdutoDAO pd = new ProdutoDAO(this);
        ArrayList<Produto> produtos = pd.FindByOutIdLista(lista.getIdLista(), categorias.get(0).getIdCategoriaProduto());
        pd.close();

        LvProdutos.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        ArrayAdapter<Produto> PrdAdapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_multiple_choice, produtos);
        LvProdutos.setAdapter(PrdAdapter);
    }

    private void CarregaListView(CategoriaProduto Categoria) {
        ArrayList<Produto> produtos;
        ProdutoDAO pd = new ProdutoDAO(this);
        produtos = pd.FindByOutIdLista(lista.getIdLista() ,Categoria.getIdCategoriaProduto());
        pd.close();

        LvProdutos.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        ArrayAdapter<Produto> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_multiple_choice, produtos);
        LvProdutos.setAdapter(adapter);

        for(int index = 0; index<LvProdutos.getCount(); index++){
            Produto prd = (Produto) LvProdutos.getItemAtPosition(index);

            if(Categoria.getProdutos() != null){
               for (Produto prdsel : Categoria.getProdutos()) {

                  if(prd.getIdProduto().equals(prdsel.getIdProduto())){
                      LvProdutos.setItemChecked(index, true);
                  }
               }
            }
        }
    }

    protected ArrayList<Produto> retornaProdutosSelecionados(){
        SparseBooleanArray clickedItemPositions = LvProdutos.getCheckedItemPositions();

        ArrayList<Produto> produtos = new ArrayList<Produto>();

        for(int index = 0; index<clickedItemPositions.size(); index++){

            boolean checked = clickedItemPositions.valueAt(index);

            if(checked){
                int key = clickedItemPositions.keyAt(index);
                Produto produto = (Produto) LvProdutos.getItemAtPosition(key);

                produtos.add(produto);
            }
        }
        return produtos;
    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v, final ContextMenu.ContextMenuInfo menuInfo) {
        MenuItem deletar = menu.add("Deletar");
        deletar.setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                final Produto produto = (Produto) LvProdutos.getItemAtPosition(info.position);

                new AlertDialog.Builder(SelecaoDeProdutoActivity.this)
                        .setMessage("Deseja deletar o produto " + produto.getNome() + "?")
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                                    @Override
                                    public void onClick(DialogInterface dialog, int which) {
                                        ProdutoDAO dao = new ProdutoDAO(SelecaoDeProdutoActivity.this);
                                        dao.Delete(produto);
                                        dao.close();

                                        CarregaListView(CategoriaAnterior);
                                    }
                                }
                        )
                        .setNegativeButton(android.R.string.no, null).show();
                return false;
            }
        });

        MenuItem editar = menu.add("Editar");
        editar.setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                Produto prd = (Produto) LvProdutos.getItemAtPosition(info.position);
                Intent intent = new Intent(SelecaoDeProdutoActivity.this, CadastroProdutoActivity.class);
                intent.putExtra("produto", prd);
                startActivity(intent);

                return false;
            }
        });
        super.onCreateContextMenu(menu, v, menuInfo);
    }

    @Override
    public void onResume() {
        super.onResume();

        CategoriaProduto categoria = (CategoriaProduto) spnCategoria.getSelectedItem();

        CarregaListView(categoria);
    }
}
