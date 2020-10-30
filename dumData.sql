INSERT INTO Bands
    ([Name])
VALUES('Tissot'),
    ('Casio'),
    ('Daniel Wellington')

INSERT INTO Categories
    (Name,SeoTitle,SeoDescription,SeoImage)
VALUES
    (N'Đồng hồ nam', N'Đồng hồ nam nổi bật', N'Đồng hô nam giá rẻ', '/images/men.jpg'),
    (N'Đồng hồ nữ', N'Đồng hồ nữ nổi bật', N'Đồng hô nữ giá rẻ', '/images/women.jpg'),
    (N'Đồng hồ đôi', N'Đồng hồ đôi nổi bật', N'Đồng hô đôi giá rẻ', '/images/couple.jpg'),
    (N'Phụ kiện', N'Phụ kiện nổi bật', N'Phụ kiện giá rẻ', '/images/couple.jpg')

INSERT INTO TypeWires
    ([Name])
VALUES
    (N'Da'),
    (N'Nhựa'),
    (N'Cao su'),
    (N'Kim loại')

INSERT INTO Products
    ([Name],[Price],[SaleCount],[Image],[CategoryID],[TypeWireID],[BandID])
VALUES('orci.', 1355668, 0, '/products/img.jpg', 2, 4, 2),
    ('eros.', 8067923, 16, '/products/img.jpg', 3, 1, 2),
    ('neque', 5238347, 17, '/products/cuff.jpg', 4, 3, 2),
    ('malesuada', 457298, 2, '/products/img.jpg', 2, 4, 3),
    ('accumsan', 1978207, 5, '/products/cuff.jpg', 4, 2, 1),
    ('Nullam', 7255523, 18, '/products/img.jpg', 2, 4, 3),
    ('mauris', 2537542, 16, '/products/cuff.jpg', 4, 2, 1),
    ('est,', 1902578, 16, '/products/img.jpg', 1, 2, 1),
    ('non', 3181081, 19, '/products/img.jpg', 3, 4, 3),
    ('venenatis', 696033, 13, '/products/img.jpg', 3, 4, 2);
INSERT INTO Products
    ([Name],[Price],[SaleCount],[Image],[CategoryID],[TypeWireID],[BandID])
VALUES('sit', 1156177, 1, '/products/img.jpg', 2, 3, 1),
    ('Etiam', 3257932, 17, '/products/img.jpg', 1, 3, 3),
    ('consequat,', 9373346, 12, '/products/img.jpg', 1, 3, 2),
    ('lacus.', 9034979, 1, '/products/img.jpg', 1, 1, 3),
    ('aliquam', 1850032, 4, '/products/cuff.jpg', 4, 2, 2),
    ('aliquam', 4084662, 9, '/products/img.jpg', 2, 4, 1),
    ('ac', 250202, 3, '/products/img.jpg', 1, 3, 3),
    ('tristique', 4748051, 8, '/products/img.jpg', 3, 3, 1),
    ('risus.', 8857540, 20, '/products/img.jpg', 2, 3, 1),
    ('Nulla', 2156864, 11, '/products/cuff.jpg', 4, 4, 3);
INSERT INTO Products
    ([Name],[Price],[SaleCount],[Image],[CategoryID],[TypeWireID],[BandID])
VALUES('neque.', 5431452, 6, '/products/img.jpg', 3, 2, 1),
    ('dapibus', 4668948, 1, '/products/img.jpg', 1, 1, 3),
    ('vel,', 8298444, 19, '/products/img.jpg', 1, 1, 2),
    ('Phasellus', 2388232, 0, '/products/img.jpg', 1, 3, 3),
    ('ante', 9710046, 11, '/products/img.jpg', 2, 1, 1),
    ('auctor.', 970229, 8, '/products/img.jpg', 3, 1, 2),
    ('Nunc', 8864917, 19, '/products/img.jpg', 1, 4, 3),
    ('et', 1019080, 1, '/products/img.jpg', 2, 2, 2),
    ('tincidunt', 316781, 4, '/products/cuff.jpg', 4, 3, 3),
    ('sociis', 7059274, 5, '/products/cuff.jpg', 4, 2, 2);
INSERT INTO Products
    ([Name],[Price],[SaleCount],[Image],[CategoryID],[TypeWireID],[BandID])
VALUES('egestas.', 1138671, 17, '/products/cuff.jpg', 4, 1, 1),
    ('aliquet', 6266114, 12, '/products/img.jpg', 1, 3, 3),
    ('vitae,', 3539761, 5, '/products/cuff.jpg', 4, 2, 2),
    ('eu,', 5446474, 0, '/products/img.jpg', 2, 4, 2),
    ('mollis', 2823801, 13, '/products/img.jpg', 1, 4, 1),
    ('Aliquam', 8362932, 20, '/products/cuff.jpg', 4, 3, 1),
    ('hymenaeos.', 5391383, 8, '/products/cuff.jpg', 4, 1, 1),
    ('dolor', 9490821, 17, '/products/img.jpg', 3, 2, 3),
    ('sapien', 931173, 13, '/products/cuff.jpg', 4, 2, 1),
    ('nascetur', 9189864, 14, '/products/img.jpg', 3, 1, 3);
INSERT INTO Products
    ([Name],[Price],[SaleCount],[Image],[CategoryID],[TypeWireID],[BandID])
VALUES('lorem', 8969620, 12, '/products/img.jpg', 2, 3, 2),
    ('vitae', 2310857, 9, '/products/cuff.jpg', 4, 4, 2),
    ('placerat', 3743771, 18, '/products/img.jpg', 1, 4, 1),
    ('eleifend', 3096851, 14, '/products/img.jpg', 1, 3, 3),
    ('malesuada', 3904263, 19, '/products/img.jpg', 1, 2, 1),
    ('Proin', 2526417, 18, '/products/img.jpg', 1, 1, 2),
    ('eget', 6778624, 18, '/products/img.jpg', 2, 3, 2),
    ('sit', 2074804, 2, '/products/cuff.jpg', 4, 3, 3),
    ('nec', 8052802, 6, '/products/img.jpg', 1, 3, 2),
    ('mauris,', 886067, 19, '/products/img.jpg', 3, 1, 1);

INSERT INTO ProductDetails([Id],[Images]) VALUES(1,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(2,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(3,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(4,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(5,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(6,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(7,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(8,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(9,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(10,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg');
INSERT INTO ProductDetails([Id],[Images]) VALUES(11,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(12,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(13,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(14,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(15,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(16,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(17,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(18,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(19,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(20,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg');
INSERT INTO ProductDetails([Id],[Images]) VALUES(21,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(22,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(23,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(24,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(25,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(26,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(27,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(28,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(29,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(30,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg');
INSERT INTO ProductDetails([Id],[Images]) VALUES(31,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(32,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(33,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(34,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(35,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(36,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(37,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(38,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(39,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(40,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg');
INSERT INTO ProductDetails([Id],[Images]) VALUES(41,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(42,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(43,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(44,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(45,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(46,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(47,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(48,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(49,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg'),(50,'/products/img.jpg ,/products/img2.jpg,/products/img3.jpg');


INSERT INTO Infos
    ([NameStore],[Logo],[Email],[Facebook],[Messenger],[Instargram],[Phone],[Address],[WorkTime],[SeoImage],[SeoTitle],[SeoDescription])
VALUES('Homer House', '/icon/logo.ico', 'dangtoan030@gmail.com', 'https://www.facebook.com/hmh0908.19/', 'https://www.facebook.com', 'https://www.facebook.com/hmh0908.19/', '0126.804.2216', N'12 Nguyễn Tri Phương, P.8, Quận 10, TP.HCM', '8:00 - 9:00', '/images/err.png', N'Chuyên đồng hô đẹp giá rẻ', N'Demo mô tả');


INSERT INTO Policies
    ([Icon],[PolicyContent])
VALUES('/icon/policy1.svg', N'Miễn phí giao hàng')
    ,
    ('/icon/policy2.svg', N'Kiểm tra hàng trước khi nhân')

INSERT INTO Fees
    ([Name],[Cost])
VALUES(N'Vận chuyển', 30000)

INSERT INTO Promotions
    ([Name],[FromDate],[ToDate],[Status],[Type])
VALUES
    ('Mua 2 sản phẩm freeship', '2020/08/08', '2020/12/12', 1, 1),
    ('Giảm giá 30%', '2020/10/10', '2020/12/12', 1, 0),
    ('Giảm giá 50%', '2020/12/12', '2020/11/12', 1, 0);

INSERT INTO PromBills
    ([ID],[Discount],[ConditionItem],[ConditionAmount])
VALUES
    (1, 0.5, 3, 5000000)

INSERT INTO PromProducts
    ([ID],[Discount],[ProductIDs])
VALUES
    (2, 100000, '1,2,3,4,5,6,7,8,9,10'),
    (3, 0.3, '7,8,9,10,11,54,21')
    


