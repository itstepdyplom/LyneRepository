"use client"
import React from 'react';
import { Box, Typography, IconButton,
  Pagination,
  Button, Grid } from '@mui/material';
import { useTranslations } from 'next-intl';
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ViewModuleIcon from "@mui/icons-material/ViewModule";
import ViewListIcon from "@mui/icons-material/ViewList";
import ShoppingBagOutlinedIcon from '@mui/icons-material/ShoppingBagOutlined';
import TuneOutlinedIcon from '@mui/icons-material/TuneOutlined';
import { useState } from "react";

const kidsItems = [
  {
    id: 1,
    name: "Cute Dinosaur T-Shirt",
    price: "500 UAH",
    image: "https://teeturtle.com/cdn/shop/files/GIL-TT-SHIRT-I_Dino_What_I_m_Doing_800x800_Flat_Women_s_Shirt.jpg?v=1734131125&width=1445",
  },
  {
    id: 2,
    name: "Pink Hoodie",
    price: "650 UAH",
    image: "https://www.gap.com/webcontent/0060/604/161/cn60604161.jpg",
  },
  {
    id: 3,
    name: "Blue Denim Jacket",
    price: "900 UAH",
    image: "https://media.johnlewiscontent.com/i/JohnLewis/009083198?fmt=auto&$background-off-white$",
  },
  {
    id: 4,
    name: "Yellow Raincoat",
    price: "750 UAH",
    image: "https://m.media-amazon.com/images/I/31AghcdKXYL.jpg",
  },
  {
    id: 5,
    name: "Red Shorts",
    price: "400 UAH",
    image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSnWarS6VWW3HCp2ZCNR-kLkLqn0Y8OPKQZow&s",
  },
  {
    id: 6,
    name: "Striped Pajamas",
    price: "550 UAH",
    image: "https://www.hannaandersson.com/dw/image/v2/BBLM_PRD/on/demandware.static/-/Sites-master-catalog/default/dw473d9200/images/main/33175/33175_S74_110_11.jpg?sw=1280&sh=1580&sm=fit&q=80",
  },
  {
    id: 7,
    name: "White Sneakers",
    price: "700 UAH",
    image: "https://img.lazcdn.com/g/p/d684c60e2bb34cb3698ad5f73d3c4252.jpg_720x720q80.jpg",
  },
  {
    id: 8,
    name: "Green Polo Shirt",
    price: "450 UAH",
    image: "https://xcdn.next.co.uk/common/items/default/default/itemimages/3_4Ratio/product/lge/C37732s5.jpg?im=Resize,width=750",
  },
  {
    id: 9,
    name: "Blue Overalls",
    price: "850 UAH",
    image: "https://cdn11.bigcommerce.com/s-r4f6haoaux/images/stencil/1280x1280/products/169/2728/224-45-toddler-bib-overall-denim-blue-KEY-front__05321.1695230832.jpg?c=1",
  },
  {
    id: 10,
    name: "Pink Dress",
    price: "950 UAH",
    image: "https://alittlelacey.com.au/cdn/shop/files/WILLOW-dusty-pink-full-length-tulle-girls-dress-med-10.jpg?v=1718682673",
  },
  {
    id: 11,
    name: "Knitted Hat",
    price: "300 UAH",
    image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQw2Rqr59PBz3Rz6y4V1yJsNI9O-cMuQcGq6A&s",
  },
  {
    id: 12,
    name: "Blue Rain Boots",
    price: "600 UAH",
    image: "https://cdn.shopify.com/s/files/1/0134/0420/9209/files/18478217084_c309aef406_o.jpg?v=1535062731",
  },
];


export default function KidsPage() {
  const t = useTranslations('Categories');
  const [view, setView] = useState<"four" | "two">("four");
  const [page, setPage] = useState(1);
  const itemsPerPage = 12;

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
          For Kids
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
        {kidsItems.slice(0, page * itemsPerPage).map((item) => (
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