import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { ProductReadComponent } from "../../components/product/product-read/product-read.component";
import { HeaderService } from '../../services/header.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [MatButtonModule, RouterModule, ProductReadComponent],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
})
export class ProductComponent implements OnInit {
  constructor(private router: Router, private headerService: HeaderService) {
    headerService.headerData = {
      title: 'Cadastro de Produtos',
      icon: 'storefront',
      routeUrl: '/products'
    }
  }

  ngOnInit(): void {}

  navigateToProductCreate(): void {
    this.router.navigate(['/products/create']);
  }
}
