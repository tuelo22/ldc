package br.com.listadecompras.listadecompras.modelo;

/**
 * Created by Julio on 30/10/2017.
 */

public enum StatusPrdEmLista {
    Pendente, Comprado;

    public static StatusPrdEmLista getStatus(int codigo) {
        return StatusPrdEmLista.values()[codigo -1];
    }
}
