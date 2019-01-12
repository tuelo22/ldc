package br.com.listadecompras.listadecompras;

import android.content.Intent;
import android.support.design.widget.TabLayout;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;

import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Collections;

import br.com.listadecompras.listadecompras.Componentes.ATProdutoEmLista;
import br.com.listadecompras.listadecompras.Util.CompareListaDePreco;
import br.com.listadecompras.listadecompras.modelo.Lista;
import br.com.listadecompras.listadecompras.modelo.ListaDePreco;
import br.com.listadecompras.listadecompras.negocio.ProcessaListaDePrecos;

public class ListaDeComprasActivity extends AppCompatActivity {

    /**
     * The {@link android.support.v4.view.PagerAdapter} that will provide
     * fragments for each of the sections. We use a
     * {@link FragmentPagerAdapter} derivative, which will keep every
     * loaded fragment in memory. If this becomes too memory intensive, it
     * may be best to switch to a
     * {@link android.support.v4.app.FragmentStatePagerAdapter}.
     */
    private SectionsPagerAdapter mSectionsPagerAdapter;

    /**
     * The {@link ViewPager} that will host the section contents.
     */
    private ViewPager mViewPager;
    private Lista lista;
    public RecyclerView RvProdutos;
    private ListaDeComprasTabPendente TabPendente;
    private ListaDeComprasTabComprado TabComprado;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_lista_de_compras);

        Intent it = getIntent();

        lista = (Lista) it.getSerializableExtra("lista");

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        // Create the adapter that will return a fragment for each of the three
        // primary sections of the activity.
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());

        // Set up the ViewPager with the sections adapter.
        mViewPager = (ViewPager) findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);

        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);

        mViewPager.addOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayout));
        tabLayout.addOnTabSelectedListener(new TabLayout.ViewPagerOnTabSelectedListener(mViewPager));

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {


                //Intent intent = new Intent(ListaDeComprasActivity.this, testeActivity.class);
                Intent intent = new Intent(ListaDeComprasActivity.this, SelecaoDeProdutoActivity.class);
                intent.putExtra("lista", lista);
                startActivity(intent);
            }
        });

        if(lista != null){
            this.setTitle(lista.getNome());
            getSupportActionBar().setTitle(lista.getNome());
        }
    }

    @Override
    protected void onResume() {

        CarregaListView();

        super.onResume();
    }
    public  void CarregaListView(){
        if(TabPendente != null) {
            TabPendente.CarregaListView();
        }

        if(TabComprado != null) {
            TabComprado.CarregaListView();
        }
    }

    /**
     * A {@link FragmentPagerAdapter} that returns a fragment corresponding to
     * one of the sections/tabs/pages.
     */
    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position){
                case 0:
                    TabPendente = new ListaDeComprasTabPendente();
                    TabPendente.setLista(lista);
                    return TabPendente;
                case 1:
                    TabComprado = new ListaDeComprasTabComprado();
                    TabComprado.setLista(lista);
                    return TabComprado;
                default:
                    return null;
            }

        }

        @Override
        public int getCount() {
            return 2;
        }

        @Override
        public CharSequence getPageTitle(int position){
            switch (position){
                case 0:
                    return "Pendente";
                case 1:
                    return "Comprado";
            }
            return null;
        }
    }
}
