package br.com.listadecompras.listadecompras.Componentes;

/**
 * Created by Julio on 02/07/2017.
 */
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.RecyclerView.Adapter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import java.util.List;
import br.com.listadecompras.listadecompras.R;
import br.com.listadecompras.listadecompras.modelo.ListaDePreco;

public class ATProdutoEmLista extends Adapter{
    private List<ListaDePreco> ListaDePrecos;
    private Context context;

    public ATProdutoEmLista(List<ListaDePreco> listaDePrecos, Context context) {
        this.ListaDePrecos = listaDePrecos;
        this.context = context;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {

        View view = LayoutInflater.from(context).inflate(R.layout.cv_produto_em_lista, parent, false);
        VHProdutoEmLista holder = new VHProdutoEmLista(view);

        return holder;
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder viewHolder, int position) {
        VHProdutoEmLista holder = (VHProdutoEmLista) viewHolder;

        ListaDePreco listaDePreco  = ListaDePrecos.get(position) ;

        holder.setcontext(context);
        holder.setListaDePreco(listaDePreco);
        holder.cvrel_nomeproduto.setText(listaDePreco.getNomeProduto());
        holder.cvrel_nomemercado.setText("Mercado: " + listaDePreco.getNomeMercado());
        holder.cvrel_preco.setText("Preço: " + listaDePreco.getPrecoString());
        holder.cvrel_quantidade.setText("Quantidade: " + listaDePreco.getQuantidadeString());
        holder.cvrel_total.setText("Total: " + listaDePreco.getTotalString());
    }

    @Override
    public int getItemCount() {
        return ListaDePrecos.size();
    }


}

