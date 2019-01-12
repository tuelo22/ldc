package br.com.listadecompras.listadecompras;

/**
 * Created by Julio on 30/10/2017.
 */

import android.content.Context;
import android.support.v4.app.Fragment;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;

import java.math.BigDecimal;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Locale;

import br.com.listadecompras.listadecompras.Componentes.ATProdutoEmLista;
import br.com.listadecompras.listadecompras.Util.CompareListaDePreco;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.ListaDePreco;
import br.com.listadecompras.listadecompras.modelo.StatusPrdEmLista;
import br.com.listadecompras.listadecompras.negocio.ProcessaListaDePrecos;

public class ListaDeComprasTabPendente extends Fragment {
    private RecyclerView RvProdutos;
    private Context contexto;
    private Lista lista;
    private AdView adView;
    private TextView totalista;

    public void setLista(Lista lista) {
        this.lista = lista;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_lista_de_compras_pendente, container, false);

        contexto = getActivity();

        RvProdutos = (RecyclerView) rootView.findViewById(R.id.rvprodutosemlistapendente);
        totalista = (TextView) rootView.findViewById(R.id.total_lista);

        adView = (AdView) rootView.findViewById(R.id.adBanner);
        AdRequest adRequest = new AdRequest.Builder().build();
        adView.loadAd(adRequest);

        CarregaListView();

        return rootView;
    }

    public void CarregaListView() {
        if(lista != null & RvProdutos != null) {
            ArrayList<ListaDePreco> listapreco = ProcessaListaDePrecos.MontaListaProdutoPreco(lista.getIdLista(), contexto, StatusPrdEmLista.Pendente);

            BigDecimal total = new BigDecimal(0);

            for(ListaDePreco itListaPreco: listapreco){

                total = total.add(itListaPreco.getTotal());
            }

            Locale locale = Locale.getDefault();

            totalista.setText(NumberFormat.getCurrencyInstance(locale).format(total));

            Collections.sort(listapreco, new CompareListaDePreco(CompareListaDePreco.POR_MERCADO));

            ATProdutoEmLista ath = new ATProdutoEmLista(listapreco, contexto);

            RvProdutos.setAdapter(ath);

            RecyclerView.LayoutManager layout = new LinearLayoutManager(contexto,LinearLayoutManager.VERTICAL, false);
            RvProdutos.setLayoutManager(layout);
        }
    }

    @Override
    public void onResume() {
        super.onResume();

        CarregaListView();

        if(adView != null){
            adView.resume();
        }
    }

    @Override
    public void onPause() {
        if(adView != null){
            adView.pause();
        }
        super.onPause();
    }

    @Override
    public void onDestroy() {
        if(adView != null){
            adView.destroy();
        }
        super.onDestroy();
    }
}
