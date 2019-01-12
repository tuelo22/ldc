package br.com.listadecompras.listadecompras.Util;

import java.util.Comparator;

import br.com.listadecompras.listadecompras.modelo.ListaDePreco;

/**
 * Created by Julio on 29/10/2017.
 */

public class CompareListaDePreco implements Comparator<ListaDePreco> {
    public static final int POR_PRODUTO = 1;
    public static final int POR_MERCADO = 2;

    int tipoComparacao;

    public CompareListaDePreco(int tipoComparacao) {
        this.tipoComparacao = tipoComparacao;
    }

    @Override
    public int compare(ListaDePreco o1, ListaDePreco o2) {
        int retorno = 0;
        switch (this.tipoComparacao) {
            case POR_PRODUTO:
                retorno = o1.getIdProduto().compareTo(o1.getIdProduto());
                break;
            case POR_MERCADO:
                retorno = o1.getNomeMercado().compareTo(o1.getNomeMercado());
                break;
            default:
                throw new RuntimeException("opcao invalida para comparar precos");
        }
        return retorno;
    }
}
