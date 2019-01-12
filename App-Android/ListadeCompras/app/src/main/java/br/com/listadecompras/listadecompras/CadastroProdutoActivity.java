package br.com.listadecompras.listadecompras;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

import br.com.listadecompras.listadecompras.dao.CategoriaProdutoDAO;
import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.CategoriaProduto;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class CadastroProdutoActivity extends AppCompatActivity {
    private Produto produto;
    private EditText edtnomproduto;
    private Spinner spnCategoria;
    private CategoriaProduto categoria;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cadastro_produto);

        edtnomproduto = (EditText) findViewById(R.id.edtnomproduto);
        spnCategoria = (Spinner) findViewById(R.id.spnCategoria);

        Intent it = getIntent();
        produto = (Produto) it.getSerializableExtra("produto");
        categoria = (CategoriaProduto) it.getSerializableExtra("categoria");

        MontaTelaInicial(produto);

        if(produto != null){
            edtnomproduto.setText(produto.getNome());
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.menu_formulario_ok:

                if(produto == null){
                    produto = new Produto();
                }
                CategoriaProduto categoria = (CategoriaProduto) spnCategoria.getSelectedItem();

                produto.setNome(edtnomproduto.getText().toString());
                produto.setIdCategoriaProdutoFK(categoria.getIdCategoriaProduto());

                ProdutoDAO pd = new ProdutoDAO(this);

                if(produto.getIdProduto() != null){
                    pd.update(produto);
                    Toast.makeText(CadastroProdutoActivity.this, "Produto atualizado !", Toast.LENGTH_SHORT).show();
                }else{
                    pd.Insere(produto);
                    Toast.makeText(CadastroProdutoActivity.this, "Produto cadastrado !", Toast.LENGTH_SHORT).show();
                }
                pd.close();

                finish();
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_cadastro_lista, menu);

        return super.onCreateOptionsMenu(menu);
    }

    private void MontaTelaInicial(Produto prd) {
        CategoriaProdutoDAO cd = new CategoriaProdutoDAO(this);
        ArrayList<CategoriaProduto> categorias = cd.FindAll();
        cd.close();

        ArrayAdapter<CategoriaProduto> CatPadapter = new ArrayAdapter<>(this,
                android.R.layout.simple_spinner_dropdown_item,
                categorias);

        CatPadapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        spnCategoria.setAdapter(CatPadapter);

        if (categoria != null) {
            CategoriaProduto cat;
            int Posicao = 0;
            int CountItem =  CatPadapter.getCount();
           for(int i = 0; i < CountItem; i++){
              cat = CatPadapter.getItem(i);

              if(cat.getIdCategoriaProduto().equals(categoria.getIdCategoriaProduto())){
                  Posicao = i;
              }
            }

            spnCategoria.setSelection(Posicao);
        }
    }
}