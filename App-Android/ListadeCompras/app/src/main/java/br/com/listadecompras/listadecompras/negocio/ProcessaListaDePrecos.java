package br.com.listadecompras.listadecompras.negocio;

import android.content.Context;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.dao.PrecoDAO;
import br.com.listadecompras.listadecompras.dao.ProdutoDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.ListaDePreco;
import br.com.listadecompras.listadecompras.modelo.Preco;
import br.com.listadecompras.listadecompras.modelo.Produto;
import br.com.listadecompras.listadecompras.modelo.StatusPrdEmLista;

/**
 * Created by Julio on 28/07/2017.
 */

public class ProcessaListaDePrecos {

    public static ArrayList<ListaDePreco> MontaListaProdutoPreco(Integer IdLista, Context Contexto, StatusPrdEmLista Status){

        ProdutoDAO pd = new ProdutoDAO(Contexto);

        ArrayList<Produto> ProdutoList = pd.FindByIdLista(IdLista);

        pd.close();

        ArrayList<ListaDePreco> listapreco =  new ArrayList<ListaDePreco>();

        for (Produto prd : ProdutoList) {
            ListaDAO ld = new ListaDAO(Contexto);
            String statusPrdEmLista = ld.FindStatusProduto(IdLista, prd.getIdProduto());
            BigDecimal quantidade = ld.FindQuantidade(IdLista, prd.getIdProduto());
            ld.close();

            if(Status.name().equals(statusPrdEmLista)) {

                ListaDePreco lp = new ListaDePreco();

                lp.setNomeProduto(prd.getNome());
                lp.setIdLista(IdLista);
                lp.setIdProduto(prd.getIdProduto());
                lp.setStatus(statusPrdEmLista);
                lp.setQuantidade(quantidade);

                PrecoDAO pcdao = new PrecoDAO(Contexto);

                Preco pc = pcdao.MenorValor(prd.getIdProduto(), IdLista);

                pcdao.close();

                if (pc != null) {
                    lp.setNomeMercado(pc.getMercado());
                    lp.setPreco(pc.getPreco());
                } else {
                    lp.setNomeMercado("Nenhum.");
                    lp.setPreco(0.0);
                }
                listapreco.add(lp);
            }
        }
        return listapreco;
    }
}
