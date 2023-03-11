


export class Category{
    categoryName:string = "";
    description:string = "";
    categoryId:string = "";
    categoryIconUrl:string = "";
    subCategories: SubCategory[]= [];
}
export class SubCategory{
    id:number = 0;
    subCategoryName : string = "";
    subCategoryUrlIcon: string = "";
    categoryId:number = 0 ;
}