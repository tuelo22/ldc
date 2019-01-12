package br.com.listadecompras.listadecompras.modelo;

import java.io.Serializable;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.Locale;

/**
 * Created by Julio on 21/04/2017.
 */

public class Preco implements Serializable {
    private Integer IdPreco;
    private Double Preco;
    private String Mercado;
    private String Observacao;
    private Integer IdProdutoFK;
    private Integer IdListaFK;
    private Produto produto;

    public Integer getIdListaFK() {
        return IdListaFK;
    }

    public void setIdListaFK(Integer idListaFK) {
        IdListaFK = idListaFK;
    }

    public Integer getIdPreco() {
        return IdPreco;
    }

    public void setIdPreco(Integer idPreco) {
        IdPreco = idPreco;
    }

    public Double getPreco() {
        return Preco;
    }

    public String getPrecoString() {
        Locale locale = Locale.getDefault();

        return NumberFormat.getCurrencyInstance(locale).format(Preco);
    }

    public void setPrecoString(String preco) {
        Locale locale = Locale.getDefault();

        String precoformatado = preco.replace(",",".")
                                     .replace(NumberFormat.getCurrencyInstance(locale).getCurrency().getSymbol(), "");

        Preco = Double.parseDouble(precoformatado);
    }

    public void setPreco(Double preco) {
        Preco = preco;
    }

    public String getMercado() {
        return Mercado;
    }

    public void setMercado(String mercado) {
        Mercado = mercado;
    }

    public String getObservacao() {
        return Observacao;
    }

    public void setObservacao(String observacao) {
        Observacao = observacao;
    }

    public Integer getIdProdutoFK() {
        return IdProdutoFK;
    }

    public void setIdProdutoFK(Integer idProdutoFK) {
        IdProdutoFK = idProdutoFK;
    }

    public Produto getProduto() {
        return produto;
    }

    public void setProduto(Produto produto) {
        this.produto = produto;
    }

    @Override
    public String toString() {
        return "Mercado: " + Mercado + " | " + getPrecoString();
    }
}

