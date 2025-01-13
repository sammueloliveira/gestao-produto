import { Department } from "./department.module";


export interface Product {
    id: string;  
    codigo: string;
    descricao: string;
    departamentos: Department; 
    preco: number | null;
    status: boolean;
   
}