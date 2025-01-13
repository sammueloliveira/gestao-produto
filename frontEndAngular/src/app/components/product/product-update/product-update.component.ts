import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../interfaces/product.module';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { Department } from '../../../interfaces/department.module';
import { DepartmentService } from '../../../services/department.service';

@Component({
  selector: 'app-product-update',
  standalone: true,
  imports: [
    RouterModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatTableModule,
    CommonModule
  ],
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.css']
})
export class ProductUpdateComponent implements OnInit {
  product: Product = { 
    id: '', 
    codigo: '', 
    descricao: '', 
    departamentos: { codigo: '', descricao: '' },
    preco: 0, 
    status: true, 

  }; 

  departamentos: Department[] = [];

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private departmentService: DepartmentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    
    this.departmentService.getDepartamentos().subscribe((departments) => {
      this.departamentos = departments;
    });

   
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.readById(id).subscribe((product) => {
        this.product = product;
      });
    }
  }

  editProduct(): void {
    console.log('Dados enviados:', this.product);
    this.productService.update(this.product).subscribe(() => {
      this.router.navigate(['/products']);
      this.productService.showMessage('Produto atualizado com sucesso!');
    });
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }
}
