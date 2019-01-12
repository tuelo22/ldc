package br.com.listadecompras.listadecompras;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import br.com.listadecompras.listadecompras.Util.MoneyTextWatcher;
import br.com.listadecompras.listadecompras.dao.PrecoDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.Preco;
import br.com.listadecompras.listadecompras.modelo.Produto;

public class CadastroPrecoActivity extends AppCompatActivity {
    private TextView lblCadPrecoNomeProduto;
    private EditText edtCadPrecoPreco;
    private EditText edtCadPrecoMercado;
    private EditText edtCadPrecoObservacao;
    private Produto produto;
    private Preco preco;
    private Integer IdLista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cadastro_preco);

        lblCadPrecoNomeProduto = (TextView) findViewById(R.id.lblCadPrecoNomeProduto);
        edtCadPrecoPreco = (EditText) findViewById(R.id.edtCadPrecoPreco);
        edtCadPrecoMercado = (EditText) findViewById(R.id.edtCadPrecoMercado);
        edtCadPrecoObservacao = (EditText) findViewById(R.id.edtCadPrecoObservacao);

        edtCadPrecoPreco.addTextChangedListener(new MoneyTextWatcher(edtCadPrecoPreco));

        Intent it = getIntent();
        produto = (Produto) it.getSerializableExtra("produto");
        preco = (Preco) it.getSerializableExtra("preco");
        IdLista = (Integer) it.getSerializableExtra("idlista");

        if(preco != null){
            CarregaPrecoNoFormulario();
        }

        if(produto != null){
            lblCadPrecoNomeProduto.setText(produto.getNome());
        }
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {
            case R.id.menu_formulario_ok:

                if(preco == null){
                    preco = new Preco();
                }

                CarregaPrecoDoFormulario();

                PrecoDAO pd = new PrecoDAO(this);

                if(preco.getIdPreco() != null){
                    pd.Update(preco);
                }else{
                    pd.Insert(preco);
                }
                pd.close();

                Toast.makeText(CadastroPrecoActivity.this, "Preço cadastrado !", Toast.LENGTH_SHORT).show();
                finish();
                break;
        }

        return super.onOptionsItemSelected(item);
    }

    private void CarregaPrecoNoFormulario(){
        edtCadPrecoPreco.setText(preco.getPrecoString());
        edtCadPrecoMercado.setText(preco.getMercado());
        edtCadPrecoObservacao.setText(preco.getObservacao());

    }
    private void CarregaPrecoDoFormulario(){
        preco.setIdProdutoFK(produto.getIdProduto());
        preco.setPrecoString(edtCadPrecoPreco.getText().toString());
        preco.setMercado(edtCadPrecoMercado.getText().toString());
        preco.setObservacao(edtCadPrecoObservacao.getText().toString());
        preco.setIdListaFK(IdLista);
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_cadastro_lista, menu);

        return super.onCreateOptionsMenu(menu);

    }
}
