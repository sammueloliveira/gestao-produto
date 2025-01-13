import { Component, OnInit, ViewChild } from '@angular/core';
import { Product } from '../../../interfaces/product.module';
import { ProductService } from '../../../services/product.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { CustomPaginatorIntl } from '../../../services/custom-paginator-intl.service';
import { Router, RouterModule } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-product-read',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    RouterModule,
    MatDialogModule,
  ],
  providers: [{ provide: MatPaginatorIntl, useClass: CustomPaginatorIntl }],
  templateUrl: './product-read.component.html',
  styleUrls: ['./product-read.component.css'],
})
export class ProductReadComponent implements OnInit {
  products!: Product[];
  displayedColumns: string[] = [
    'codigo',
    'descricao',
    'departamentos',
    'preco',
    'status',
    'action',
  ];

  dataSource = new MatTableDataSource<Product>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private productService: ProductService,
    private router: Router,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.productService.read().subscribe((products) => {
      this.products = products;
      this.dataSource.data = products;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  viewProduct(id: string): void {
    this.productService.readById(id).subscribe((product) => {
      this.router.navigate(['/products', 'detail', product.id]);
    });
  }

  deleteProduct(id: string): void {
    if (id) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this.productService.delete(id).subscribe(() => {
            this.products = this.products.filter(
              (product) => product.id !== id
            );
            this.dataSource.data = this.products;
            this.productService.showMessage(`Produto deletado com sucesso!`);
          });
        }
      });
    } else {
      this.productService.showMessage('Erro ao deletar produto!');
    }
  }
}
