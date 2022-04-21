let cookieName = "basket-item";
function addToBasket(id, name, picture, price,slug) {
    let products = $.cookie(cookieName);

    if (products !== undefined) {
        products = JSON.parse(products);
    }
    else {
        products=[]
    }
    let count = $("#productCount").val()
   
    let currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        currentProduct.count = parseInt(currentProduct.count) + parseInt(count);
    }
    else {
        const product = {
            id,
            name,
            picture,
            unitPrice:price,
            count,
            slug
        }
        products.push(product);
    };
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/",});

    updateBasket();
}

function updateBasket() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#current_count_in_basket").text(products.length);
    $("#current_count_in_basket_mobile").text(products.length)
    const cartItems = $("#cart-item");
    cartItems.html('');
    products.forEach(product => {
        var html = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromBasket('${product.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/UploadPictures/${product.picture}"
                                         class="img-fluid" alt="">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${product.name}</a>
                                </p>
                                <p class="count">تعداد: ${product.count}</p>
                                <p class="count">قیمت واحد: ${product.unitPrice}</p>
                            </div>
                        </div>`;

        cartItems.append(html);
    })
}

function removeFromBasket(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    currentProduct = products.findIndex(x => x.id === id);
    if (currentProduct !== undefined) {
        products.splice(currentProduct,1)
    };
    $.cookie(cookieName, JSON.stringify(products), {expires:2,path:"/"})
    updateBasket();
}

function checkCountProduct(id, totalPriceId, count) {
    //let products = $.cookie(cookieName);
    //products = JSON.parse(products);
    //let productIndex = products.findIndex(x => x.id == id);
    //let product = products[productIndex];
    //product.count = count;
    //var newPrice = parseInt(count) * parseInt(product.unitPrice);
    //$(`#${totalPriceId}`).text(newPrice);

    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    var productIndex = products.findIndex(x => x.id == id);
    var currentProduct = products[productIndex]
    currentProduct.count = count;
    let totalPrice = parseInt(count) * parseInt(currentProduct.unitPrice)
    $(`#${totalPriceId}`).text(totalPrice);

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" })
    updateBasket()
}























////////let cookieName = "basket-item";


////function addToBasket(id, name, picture, price) {
////    let products = $.cookie(cookieName);
////    if (products === undefined) {
////        products = [];
////    } else {
////        products = JSON.parse(products);
////    }

////    const count = $("#productCount").val();
////    const currentProduct = products.find(x => x.id === id);
////    if (currentProduct !== undefined) {
////        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
////    } else {
////        const product = {
////            id,
////            name,
////            unitPrice: price,
////            picture,
////            count
////        }

////        products.push(product);
////    }

////    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
////    updateBasket();
////}

////function updateBasket() {
////    debugger;
////    let products = $.cookie(cookieName);
////    products = JSON.parse(products);
////    $("#current_count_in_basket").text(products.length)
////    const cartItems = $("#cart-item");
////    cartItems.html('');
////    products.forEach(x => {
////        const product = `<div class="single-cart-item">
////                            <a href="javascript:void(0)" class="remove-icon" onclick="removeItem('${x.id}')">
////                                <i class="ion-android-close"></i>
////                            </a>
////                            <div class="image">
////                                <a href="single-product.html">
////                                    <img src="/ProductPictures/${x.picture}"
////                                         class="img-fluid" alt="">
////                                </a>
////                            </div>
////                            <div class="content">
////                                <p class="product-title">
////                                    <a href="single-product.html">محصول: ${x.name}</a>
////                                </p>
////                                <p class="count">تعداد: ${x.count}</p>
////                                <p class="count">قیمت واحد: ${x.unitPrice}</p>
////                            </div>
////                        </div>`;
////        cartItems.append(product);
////    });
////}

////function removeItem(id) {
////    let products = $.cookie(cookieName);
////    debugger;
////    products = JSON.parse(products);
////    let currentProduct = products.findIndex(x => x.id === id)
////    products.splice(currentProduct, 1);
////    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
////    updateBasket();
////    }
