package br.com.listadecompras.listadecompras.modelo;

import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.Comparator;
import java.util.Locale;

/**
 * Created by Julio on 02/07/2017.
 */

public class ListaDePreco{
    private String NomeProduto;
    private String NomeMercado;
    private Double Preco;
    private Integer IdProduto;
    private Integer IdLista;
    private BigDecimal Quantidade;

    public BigDecimal getQuantidade() {
        return Quantidade;
    }

    public void setQuantidade(BigDecimal quantidade) {
        Quantidade = quantidade;
    }

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        Status = status;
    }

    private String Status;

    public String getNomeProduto() {
        return NomeProduto;
    }

    public void setNomeProduto(String nomeProduto) {
        NomeProduto = nomeProduto;
    }

    public String getNomeMercado() {
        return NomeMercado;
    }

    public void setNomeMercado(String nomeMercado) {
        NomeMercado = nomeMercado;
    }

    public Double getPreco() {
        return Preco;
    }

    public void setPreco(Double preco) {
        Preco = preco;
    }

    public Integer getIdProduto() {
        return IdProduto;
    }

    public void setIdProduto(Integer idProduto) {
        IdProduto = idProduto;
    }

    public Integer getIdLista() {
        return IdLista;
    }

    public void setIdLista(Integer idLista) {
        IdLista = idLista;
    }

    public String getPrecoString() {
        Locale locale = Locale.getDefault();

        return NumberFormat.getCurrencyInstance(locale).format(Preco);
    }

    public String getQuantidadeString() {
        DecimalFormat format = new DecimalFormat("0.000");
        return format.format(Quantidade).replace(".",",");
    }

    public BigDecimal getTotal(){
        return Quantidade.multiply(BigDecimal.valueOf(Preco)).setScale(
                2, BigDecimal.ROUND_FLOOR);
    }

    public String getTotalString(){
        Locale locale = Locale.getDefault();

        return NumberFormat.getCurrencyInstance(locale).format(getTotal());
    }

}
