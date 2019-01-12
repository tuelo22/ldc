package br.com.listadecompras.listadecompras.modelo;

import java.io.Serializable;
import java.util.List;

/**
 * Created by Julio on 28/01/2017.
 */

public class Produto implements Serializable {
    private Integer IdCategoriaProdutoFK;
    private Integer IdProduto;
    private String Nome;

    public String getNome() {
        return Nome;
    }

    public void setNome(String nome) {
        Nome = nome;
    }

    public Integer getIdProduto() {
        return IdProduto;
    }

    public void setIdProduto(Integer idProduto) {
        IdProduto = idProduto;
    }

    public Integer getIdCategoriaProdutoFK() {
        return IdCategoriaProdutoFK;
    }

    public void setIdCategoriaProdutoFK(Integer idCategoriaProdutoFK) {
        IdCategoriaProdutoFK = idCategoriaProdutoFK;
    }

    @Override
    public String toString() {
        return Nome;
    }
}
