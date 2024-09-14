import { Component, inject } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
  import {
    MAT_DIALOG_DATA,
    MatDialog,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogModule,
    MatDialogRef,
    MatDialogTitle,
  } from '@angular/material/dialog';
import { AddProductComponent } from '../add-product/add-product.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatDialogModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  
  readonly dialog = inject(MatDialog);

openAddProductDialog(): void {
  const dialogRef = this.dialog.open(AddProductComponent, {
    
  });

  dialogRef.afterClosed().subscribe(result => {
  });
}
}
