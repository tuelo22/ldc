package br.com.listadecompras.listadecompras.modelo;

import java.io.Serializable;
import java.util.List;

/**
 * Created by Julio on 28/01/2017.
 */

public class Lista implements Serializable {
    private Integer IdLista;
    private String Nome;
    private String Descricao;
    private String DataCadastro;

    private List<Produto> Produtos;

    public Integer getIdLista() {
        return IdLista;
    }

    public void setIdLista(Integer idLista) {
        IdLista = idLista;
    }

    public String getNome() {
        return Nome;
    }

    public void setNome(String nome) {
        Nome = nome;
    }

    public String getDescricao() {
        return Descricao;
    }

    public void setDescricao(String descricao) {
        Descricao = descricao;
    }

    public String getDataCadastro() {
        return DataCadastro;
    }

    public void setDataCadastro(String dataCadastro) {
        DataCadastro = dataCadastro;
    }

    public List<Produto> getProdutos() {
        return Produtos;
    }

    public void setProdutos(List<Produto> produtos) {
        Produtos = produtos;
    }

    @Override
    public String toString() {
        return IdLista + " - " + Nome;
    }
}
