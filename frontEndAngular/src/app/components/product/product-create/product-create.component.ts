import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { DepartmentService } from '../../../services/department.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { Product } from '../../../interfaces/product.module';
import { Department } from '../../../interfaces/department.module';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-create',
  standalone: true, 
  imports: [
    RouterModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    CommonModule
  ],
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css'],
})
export class ProductCreateComponent implements OnInit {
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
    private productService: ProductService,
    private departmentService: DepartmentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadDepartments();  
  }
  
  loadDepartments(): void {
    this.departmentService.getDepartamentos().subscribe(
      (data) => {
        this.departamentos = data; 
      },
      (error) => {
        console.error('Error loading departments:', error);
      }
    );
  }

 
  createProduct(form: NgForm): void {
    if (form.valid) {
      this.productService.create(this.product).subscribe({
        next: () => {
          this.productService.showMessage('Produto cadastrado com sucesso!');
          this.router.navigate(['/products']);
        },
        error: () => {
          this.productService.showMessage('Erro ao cadastrar produto!');
        },
      });
    } else {
      form.form.markAllAsTouched();
      this.productService.showMessage('Preencha todos os campos obrigatórios.');
    }
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }
}
