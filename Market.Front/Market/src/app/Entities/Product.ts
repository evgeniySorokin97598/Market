
export class Product{
   public id:number =0;
   public name:string = "";
   public description:string = "";
   public price:number = 0; 
   public image:string = ""; //// ссылка на картинку
   public quantity:number = 0;
   public brend:string = "";
   public comments:CommentEntity[] = [];
   public typesCharacteristics:CharacteristicType[] = []; ///массив с характеристиками товара
   
}
export class CommentEntity{
   public dignity:string = ""
   public comment:string = "";
   public flaws:string = "";
   public productId:number = 0;
   public stars:number = 0;
   public userName:string = "";
   public countLikes:number = 0;
   public commentId:number = 0;
}

export class CharacteristicType{
   public name:string = "";
   public charastitics: Charastitic [] =  [];
}

export class Charastitic{
   public name:string = "";
   public text: string = "";
}