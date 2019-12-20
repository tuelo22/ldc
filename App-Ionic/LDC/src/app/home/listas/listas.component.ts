import { ListaService } from './lista.service';
import { Component, OnInit, Input } from '@angular/core';
import { Lista } from './lista.model';

@Component({
  selector: 'app-listas',
  templateUrl: './listas.component.html'
})
export class ListasComponent implements OnInit {

  @Input() user: string

  Listas: Lista[] = []

  constructor(private listaService: ListaService) { }

  ngOnInit() {
    this.listaService.getlistas(this.user).subscribe(listas => this.Listas = listas)

    console.log(this.Listas)

    this.Listas.forEach(l => {
      console.log(l)
    });
    
  }
}
