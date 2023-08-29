import { inject, Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ProductModel } from 'src/app/models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  
  #httpClient = inject(HttpClient);
  products!: ProductModel[];


  ngOnInit(): void {
    this.#httpClient.get<ProductModel[]>('https://localhost:7061/api/Products').subscribe((products) => {
      this.products = products;
    });
  }
}
