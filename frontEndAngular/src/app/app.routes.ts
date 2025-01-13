import { Routes } from '@angular/router';
import { HomeComponent } from './views/home/home.component';
import { ProductComponent } from './views/product/product.component';
import { ProductCreateComponent } from './components/product/product-create/product-create.component';
import { ProductDetailComponent } from './components/product/product-details/product-detail.component';
import { ProductUpdateComponent } from './components/product/product-update/product-update.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'products', 
    component: ProductComponent
  },
  { path: 'products/detail/:id', component: ProductDetailComponent },
  {
    path: 'products/create', 
    component: ProductCreateComponent
  },
  { path: 'products/update/:id', component: ProductUpdateComponent },
];
