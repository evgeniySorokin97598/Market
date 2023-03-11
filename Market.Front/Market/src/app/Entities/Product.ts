
export class Product{
   public id:number =0;
   public name:string = "";
   public description:string = "";
   public price:number = 0; 
   public image:string = ""; //// ссылка на картинку
   public quantity:number = 0;
   public brend:string = "";
   
   public typesCharacteristics:CharacteristicType[] = []; ///массив с характеристиками товара

}

export class CharacteristicType{
   public name:string = "";
   public charastitics: Charastitic [] =  [];
}

export class Charastitic{
   public name:string = "";
   public text: string = "";
}