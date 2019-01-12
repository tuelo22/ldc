package br.com.listadecompras.listadecompras.Componentes;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.text.Editable;
import android.text.TextWatcher;
import android.text.method.DigitsKeyListener;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;
import java.util.Locale;

import br.com.listadecompras.listadecompras.CadastroListaActivity;
import br.com.listadecompras.listadecompras.ListaDeComprasActivity;
import br.com.listadecompras.listadecompras.ListaPrecosActivity;
import br.com.listadecompras.listadecompras.R;
import br.com.listadecompras.listadecompras.Util.MoneyTextWatcher;
import br.com.listadecompras.listadecompras.Util.QuantidadeTextWatcher;
import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.PrecoDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.ListaDePreco;
import br.com.listadecompras.listadecompras.modelo.Preco;
import br.com.listadecompras.listadecompras.modelo.Produto;
import br.com.listadecompras.listadecompras.modelo.StatusPrdEmLista;

/**
 * Created by Julio on 02/07/2017.
 */

public class VHProdutoEmLista extends ViewHolder implements View.OnClickListener{
    private ListaDePreco listaDePreco;
    private Context context;
    private View VHolder;

    public void setListaDePreco(ListaDePreco listaDePreco) {
        this.listaDePreco = listaDePreco;
    }
    public void setcontext(Context context) {
        this.context = context;
    }

    public final TextView cvrel_nomeproduto;
    public final TextView cvrel_nomemercado;
    public final TextView cvrel_preco;
    public final TextView cvrel_quantidade;
    public final TextView cvrel_total;

    public final Button cvrel_btn_delete;
    public final Button cvrel_btn_precos;
    public final Button cvrel_btn_quantidade;
    public final Button cvrel_btn_comprado;

    public VHProdutoEmLista(View view) {
        super(view);
        cvrel_nomeproduto = (TextView) view.findViewById(R.id.cvrel_nomeproduto);
        cvrel_nomemercado = (TextView) view.findViewById(R.id.cvrel_nomemercado);
        cvrel_preco = (TextView) view.findViewById(R.id.cvrel_preco);
        cvrel_quantidade = (TextView) view.findViewById(R.id.cvrel_quantidade);
        cvrel_total = (TextView) view.findViewById(R.id.cvrel_total);

        cvrel_btn_quantidade = (Button) view.findViewById(R.id.cvrel_btn_quantidade);
        cvrel_btn_delete = (Button) view.findViewById(R.id.cvrel_btn_delete);
        cvrel_btn_precos = (Button) view.findViewById(R.id.cvrel_btn_precos);
        cvrel_btn_comprado = (Button) view.findViewById(R.id.cvrel_btn_comprado);

        VHolder = view;

        cvrel_btn_delete.setOnClickListener(this);
        cvrel_btn_precos.setOnClickListener(this);
        cvrel_btn_comprado.setOnClickListener(this);
        cvrel_btn_quantidade.setOnClickListener(this);

        view.setOnClickListener(this);
    }

    @Override
    public void onClick(final View v) {
        if (v.getId() == VHolder.getId()){
            // Click da View
            ProdutoDAO pd = new ProdutoDAO(context);
            Produto prd = pd.FindById(listaDePreco.getIdProduto());

            Intent intent = new Intent(context, ListaPrecosActivity.class);
            intent.putExtra("produto", prd);
            intent.putExtra("idlista", listaDePreco.getIdLista());
            context.startActivity(intent);

        }else if (v.getId() == cvrel_btn_quantidade.getId()) {
            // Click da Quantidade
            LayoutInflater inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            AlertDialog.Builder mBuilder = new AlertDialog.Builder(context);

            final View mView = inflater.inflate(R.layout.dialog_insere_quantidade, null);
            final EditText edtInsereQuantidade = (EditText) mView.findViewById(R.id.edtInsereQuantidade);

            edtInsereQuantidade.addTextChangedListener(new QuantidadeTextWatcher(edtInsereQuantidade));

            mBuilder.setView(mView)
                    .setTitle(listaDePreco.getNomeProduto())
                    .setNegativeButton("Cancelar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {

                        }

                    })
                    .setPositiveButton("Salvar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            if (edtInsereQuantidade.getText().toString().isEmpty()){
                                new AlertDialog.Builder(context)
                                        .setMessage("Quantidade obrigatória !")
                                        .show();
                            }else{
                                ListaDAO ld = new ListaDAO(context);
                                BigDecimal qtd = new BigDecimal(edtInsereQuantidade.getText().toString().replace(",","."));
                                ld.UpdateQuantidade(listaDePreco.getIdLista(), listaDePreco.getIdProduto(), qtd);
                                ld.close();

                                ((ListaDeComprasActivity)context).CarregaListView();

                                Toast.makeText(context, "Quantidade informada para o produto " + listaDePreco.getNomeProduto() + ".", Toast.LENGTH_SHORT).show();

                            }

                        }
                    });

            AlertDialog dialog = mBuilder.create();
            dialog.show();

        }else if (v.getId() == cvrel_btn_precos.getId()) {
            // Click da Preço
            LayoutInflater inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            AlertDialog.Builder mBuilder = new AlertDialog.Builder(context);
            final View mView = inflater.inflate(R.layout.dialog_cadastro_preco, null);
            final EditText edtCadPrecoPreco = (EditText) mView.findViewById(R.id.edtCadPrecoPreco);
            final EditText edtCadPrecoMercado = (EditText) mView.findViewById(R.id.edtCadPrecoMercado);
            final EditText edtCadPrecoObservacao = (EditText) mView.findViewById(R.id.edtCadPrecoObservacao);

            edtCadPrecoPreco.addTextChangedListener(new MoneyTextWatcher(edtCadPrecoPreco));

            mBuilder.setView(mView)
                    .setTitle(listaDePreco.getNomeProduto())
                    .setNegativeButton("Cancelar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {

                        }

                    })
                    .setPositiveButton("Salvar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            if (edtCadPrecoPreco.getText().toString().isEmpty()){
                                new AlertDialog.Builder(context)
                                        .setMessage("Preço obrigatório !")
                                        .show();
                            }else if(edtCadPrecoMercado.getText().toString().isEmpty()){
                                new AlertDialog.Builder(context)
                                        .setMessage("Mercado obrigatório !")
                                        .show();
                            }else {
                                Preco preco = new Preco();
                                preco.setIdListaFK(listaDePreco.getIdLista());
                                preco.setIdProdutoFK(listaDePreco.getIdProduto());
                                preco.setPrecoString(edtCadPrecoPreco.getText().toString());
                                preco.setMercado(edtCadPrecoMercado.getText().toString());
                                preco.setObservacao(edtCadPrecoObservacao.getText().toString());

                                PrecoDAO pd = new PrecoDAO(context);

                                if(preco.getIdPreco() != null){
                                    pd.Update(preco);
                                }else{
                                    pd.Insert(preco);
                                }
                                pd.close();

                                ((ListaDeComprasActivity)context).CarregaListView();

                                Toast.makeText(context, "Peço cadastrado para o Produto " + listaDePreco.getNomeProduto() + ".", Toast.LENGTH_SHORT).show();
                            }
                        }
                    });

            AlertDialog dialog = mBuilder.create();
            dialog.show();

        }else if(v.getId() == cvrel_btn_comprado.getId()){
            // Click da Comprado

            final String status;

            if (listaDePreco.getStatus().equals(StatusPrdEmLista.Pendente.name())){
                status = StatusPrdEmLista.Comprado.name();
            }else {
                status = StatusPrdEmLista.Pendente.name();
            }

            new AlertDialog.Builder(context)
                    .setMessage("Deseja marcar o produto como " +status+ "?")
                    .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog, int which) {
                                    ListaDAO ld = new ListaDAO(context);
                                    ld.UpdateStatus(listaDePreco.getIdLista(), listaDePreco.getIdProduto(), status);
                                    ld.close();
                                    Snackbar.make(v, "Produto " + listaDePreco.getNomeProduto() + " marcado como " + status + ".", Snackbar.LENGTH_LONG)
                                            .setAction("Action", null).show();

                                    ((ListaDeComprasActivity)context).CarregaListView();
                                }
                            }
                    )
                    .setNegativeButton(android.R.string.no, null).show();
        }else if (v.getId() == cvrel_btn_delete.getId()){
            // Click da Delete
            new AlertDialog.Builder(context)
                    .setMessage("Deseja remover " +listaDePreco.getNomeProduto()+ " da Lista ?")
                    .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog, int which) {
                                    ListaDAO ld = new ListaDAO(context);
                                    ld.DeleteProdutoLista(listaDePreco.getIdLista(), listaDePreco.getIdProduto());
                                    ld.close();
                                    Snackbar.make(v, listaDePreco.getNomeProduto() + " removido !", Snackbar.LENGTH_LONG)
                                            .setAction("Action", null).show();

                                    ((ListaDeComprasActivity) context).CarregaListView();

                                }
                            }
                    )
                    .setNegativeButton(android.R.string.no, null).show();
        }
    }
}
