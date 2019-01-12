package br.com.listadecompras.listadecompras.Componentes;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.math.BigDecimal;
import java.text.NumberFormat;
import java.util.List;
import java.util.Locale;

import br.com.listadecompras.listadecompras.R;
import br.com.listadecompras.listadecompras.dao.ListaDAO;
import br.com.listadecompras.listadecompras.modelo.Lista;

/**
 * Created by Julio on 21/12/2017.
 */

public class ATLista extends BaseAdapter {
    private final List<Lista> listas;
    private final Activity act;

    public ATLista(List<Lista> listas, Activity act) {
        this.listas = listas;
        this.act = act;
    }

    @Override
    public int getCount() {
        return listas.size();
    }

    @Override
    public Object getItem(int position) {
        return listas.get(position);
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = act.getLayoutInflater().inflate(R.layout.layout_listview_tres_subtitulos, parent, false);

        Lista lista = listas.get(position);

        Locale locale = Locale.getDefault();

        ListaDAO ld = new ListaDAO(act);
        BigDecimal Total = ld.FindValorTotalLista(lista.getIdLista());

        TextView  tres_subtitulos_h1 =  (TextView)view.findViewById(R.id.tres_subtitulos_h1);
        TextView  tres_subtitulos_h2 =  (TextView)view.findViewById(R.id.tres_subtitulos_h2);
        TextView  tres_subtitulos_h3 =  (TextView)view.findViewById(R.id.tres_subtitulos_h3);

        tres_subtitulos_h1.setText(lista.getNome());
        tres_subtitulos_h2.setText(lista.getDataCadastro());
        tres_subtitulos_h3.setText(NumberFormat.getCurrencyInstance(locale).format(Total));

        return view;
    }
}
