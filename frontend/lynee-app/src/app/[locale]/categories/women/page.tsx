'use client';
import React from 'react';
import {Box,
  Grid,
  Typography,
  IconButton,
  Pagination,
  Button,
 } from '@mui/material';
import { useState } from "react";
import { useTranslations } from 'next-intl';
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ViewModuleIcon from "@mui/icons-material/ViewModule";
import ViewListIcon from "@mui/icons-material/ViewList";
import ShoppingBagOutlinedIcon from '@mui/icons-material/ShoppingBagOutlined';
import TuneOutlinedIcon from '@mui/icons-material/TuneOutlined';
import { productsAPI } from "@/services/api";
interface ProductCardVm {
  id: string;
  name: string;
  brand: string;
  price: number;
  image: string;
  background?: string;
}
const testItems = [
  {
    id: 1,
    name: "Elegant Floral Dress",
    price: "2580 UAH",
    image: "https://i.pinimg.com/736x/21/4e/e9/214ee9a06b995ef362c7df87e1bbdbf7.jpg",
  },
  {
    id: 2,
    name: "Grey T-Shirt",
    price: "850 UAH",
    image: "https://www.nextlevelapparel.com/cdn/shop/files/7610_HeatherGray_Womens_F_M-2910.jpg?v=1745862738&width=2048",
  },
  {
    id: 3,
    name: "Black Mini Dress",
    price: "3150 UAH",
    image: "https://cdn.shopify.com/s/files/1/0319/9247/9803/files/EBONY-MINI-DRESS---BLACK-_3.jpg.webp?v=1718761385",
  },
  {
    id: 4,
    name: "Navy Long Dress",
    price: "4900 UAH",
    image: "https://www.pinkboutique.co.uk/cdn/shop/files/sophisticated-illusion-navy-plunge-front-split-maxi-dress_3_e4f88a4a-cc97-44db-8901-5c024f2c92c5.jpg?v=1732885022&width=2048",
  },
  {
    id: 5,
    name: "Flat Sandals",
    price: "2300 UAH",
    image: "https://static.e-stradivarius.net/assets/public/d8bc/1368/524f4ec99b8c/1e857869876b/19750671091-a2/19750671091-a2.jpg?ts=1744730314218&w=1082&f=auto",
  },
  {
    id: 6,
    name: "White Floral Dress",
    price: "2990 UAH",
    image: "https://www.selfieleslie.com/cdn/shop/products/62157bk03_white-4_1365x.jpg?v=1700786680",
  },
  {
    id: 7,
    name: "Yellow Bikini Set",
    price: "1550 UAH",
    image: "https://palmsbikini.com/wp-content/uploads/2024/08/Micro-Bikini-Push-up-Bikini-Set-Yellow-Bikini-Brazilian-Bikini-Thong-Bikini-Woman-Bikini-PALMS.-Bikini.webp",
  },
  {
    id: 8,
    name: "Black Leather Belt",
    price: "990 UAH",
    image: "https://black-brown.com/cdn/shop/collections/Naomi_black.jpg?v=1675875745",
  },
  {
    id: 9,
    name: "Black Bag",
    price: "1990 UAH",
    image: "https://international.victoriabeckham.com/cdn/shop/files/UntitledSession20333_1500x.jpg?v=1725286028",
  },
  {
    id: 10,
    name: "White Bra",
    price: "760 UAH",
    image: "https://negativeunderwear.com/cdn/shop/products/Bra_CottonBraTop_White_Ksenia_01.jpg?v=1647464371",
  },
  {
    id: 11,
    name: "Beige Sweater",
    price: "1750 UAH",
    image: "https://image.hm.com/assets/hm/88/1f/881f902dd390297dac211f2b5eafa9266443e4e0.jpg?imwidth=2160",
  },
  {
    id: 12,
    name: "Elegant Brown Suit",
    price: "3500 UAH",
    image: "https://images.hugoboss.com/is/image/boss/hbeu50544133_201_350?$large$=&fit=crop,1&align=1,1&bgcolor=ebebeb&lastModified=1764250738000&qlt=80&resMode=sharp2&wid=338",
  },
];
export default function WomenPage() {
  const [view, setView] = useState<"four" | "two">("four");
  const [page, setPage] = useState(1);
  const [loadingNew, setLoadingNew] = React.useState(true);
  const [errorNew, setErrorNew] = React.useState<string | null>(null);
  const [products, setProducts] = React.useState<ProductCardVm[]>([]);
  const itemsPerPage = 12;
  const toCardVm = React.useCallback(
      (p: import("@/services/api").Product): ProductCardVm => ({
        id: String(p.id),
        name: p.name,
        brand: p.brand,
        price: p.price,
        image: p.imageUrl || "/img/placeholder.png",
        background: "/img/background.png",
      }),
      []
    );
React.useEffect(() => {
  const controller = new AbortController();

  const load = async () => {
    setLoadingNew(true);
    setErrorNew(null);

    try {
      const res = await productsAPI.getAll(
        { page: 1, limit: 4, categoryName: "For Her" },
        controller.signal
      );

      const list = res.items ?? [];
      setProducts(list.map(toCardVm));
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (e: any) {
      if (e?.name === "CanceledError" || e?.code === "ERR_CANCELED") return;

      setErrorNew("Failed to load products");
      setProducts([]);
      console.error(e);
    } finally {
      setLoadingNew(false);
    }
  };load();
  return () => controller.abort();
}, [toCardVm]);
  const handleChangePage = (_: any, value: number) => setPage(value);

  return (
    <Box sx={{ width: "100%", p: 3 }}>
      {/* Header */}
      <Box
        sx={{
          mb: 3,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Typography sx={{ fontSize: 16, fontWeight:400 }}>
          <IconButton size="medium">
                <TuneOutlinedIcon fontSize="medium" />
            </IconButton>
            Filters & Sort
        </Typography>
        <Typography variant="h6" sx={{ fontWeight: 400, fontSize:24, mr:12 }}>
          For Her
        </Typography>

        <Box sx={{ display: "flex", gap: 1 }}>
          <IconButton onClick={() => setView("four")}>
            <ViewModuleIcon />
          </IconButton>
          <IconButton onClick={() => setView("two")}>
            <ViewListIcon />
          </IconButton>
        </Box>
      </Box>

      {/* Grid */}
      <Grid
        container
        spacing={3}
        sx={{
          display: "grid",
          gridTemplateColumns:
            view === "four"
        ? { xs: "1fr", sm: "1fr 1fr", md: "repeat(4, 1fr)" } 
        : { xs: "1fr", sm: "1fr 1fr", md: "1fr 1fr" },
          gap: 3,
        }}
      >
        {products.slice(0, page * itemsPerPage).map((item) => (
          <Box
            key={item.id}
            sx={{
              borderRadius: 2,
              overflow: "hidden",
              position: "relative",
              background: "#fff",
              boxShadow: "0 2px 5px rgba(0,0,0,0.05)",
            }}
          >
            {/* Top Icons */}
            <Box
              sx={{
                position: "absolute",
                top: 10,
                right: 10,
                display: "flex",
                gap: 1,
              }}
            >
              <IconButton size="medium">
                <ShoppingBagOutlinedIcon fontSize="medium" />
              </IconButton>
              <IconButton size="medium">
                <FavoriteBorderIcon fontSize="medium" />
              </IconButton>
            </Box>

            <Box 
              component="img"
              src={item.image}
              alt={item.name} 
              sx={{
                width: "100%",
                height: view === "four" ? 380 : 500,
                objectFit: "cover",
                backgroundColor:"#F8F8F8"
              }} >
            </Box>
            {/* Title + Price */}
            <Box sx={{ p: 2, display:"flex", flexDirection:"column", alignItems:"center" }}>
              <Typography sx={{ fontSize: 16, color:"#2C2B2B" }}>{item.name}</Typography>
              <Typography sx={{ fontSize: 16, fontWeight: 400, color:"#7B8487"  }}>
                {item.price}
              </Typography>
            </Box>
          </Box>
        ))}
      </Grid>

      {/* Load more */}
      <Box sx={{ textAlign: "center", mt: 4 }}>
        <Button variant="text" sx={{color:"#817E7E"}}>Load more</Button>

        <Pagination
          count={5}
          page={page}
          onChange={handleChangePage}
          sx={{ mt: 2, display: "flex", justifyContent: "center", fontSize:16, fontWeight:300, color:"#817E7E" }}
        />
      </Box>
    </Box>
  );
} 