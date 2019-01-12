package br.com.listadecompras.listadecompras.modelo;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Objects;

/**
 * Created by Julio on 04/11/2017.
 */

public class CategoriaProduto implements Serializable {
    private Integer IdCategoriaProduto;
    private String Nome;
    private ArrayList<Produto> Produtos;

    @Override
    public String toString() {
        return Nome;
    }

    public Integer getIdCategoriaProduto() {
        return IdCategoriaProduto;
    }

    public void setIdCategoriaProduto(Integer idCategoriaProduto) {
        IdCategoriaProduto = idCategoriaProduto;
    }

    public String getNome() {
        return Nome;
    }

    public void setNome(String nome) {
        Nome = nome;
    }

    public ArrayList<Produto> getProdutos() {
        return Produtos;
    }

    public void setProdutos(ArrayList<Produto> produtos) {
        Produtos = produtos;
    }
}
