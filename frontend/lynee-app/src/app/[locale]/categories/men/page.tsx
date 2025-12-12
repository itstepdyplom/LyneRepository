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
const menItems = [
  {
    id: 1,
    name: "Classic White Shirt",
    price: "1200 UAH",
    image: "https://cdn.shopify.com/s/files/1/1025/3059/files/ClassicWhiteShirt_Front_1440x2000_crop_center.jpg?v=1764904624",
  },
  {
    id: 2,
    name: "Grey Hoodie",
    price: "950 UAH",
    image: "https://xcdn.next.co.uk/common/items/default/default/itemimages/3_4Ratio/product/lge/740089s5.jpg?im=Resize,width=750",
  },
  {
    id: 3,
    name: "Black Leather Jacket",
    price: "4200 UAH",
    image: "https://barneysoriginals.com/wp-content/uploads/2019/01/WEASLEY-BLCK-0047h-scaled.jpg",
  },
  {
    id: 4,
    name: "Navy Suit",
    price: "5600 UAH",
    image: "https://xcdn.next.co.uk/common/items/default/default/itemimages/3_4Ratio/product/lge/811264s.jpg?im=Resize,width=750",
  },
  {
    id: 5,
    name: "Brown Chinos",
    price: "1500 UAH",
    image: "https://xcdn.next.co.uk/common/items/default/default/itemimages/3_4Ratio/product/lge/689622s.jpg?im=Resize,width=750",
  },
  {
    id: 6,
    name: "Denim Jeans",
    price: "1800 UAH",
    image: "https://www.tenuejeans.com/cdn/shop/files/JACKSONRENO_0051.jpg?v=1746526707&width=1024",
  },
  {
    id: 7,
    name: "Black Sneakers",
    price: "2500 UAH",
    image: "https://martinvalen.com/27408-mv_large_default/chunky-sneakers-shoes-all-black.jpg",
  },
  {
    id: 8,
    name: "Blue Polo Shirt",
    price: "1100 UAH",
    image: "https://caslay.in/cdn/shop/files/0W2A6470.jpg?v=1761469179&width=1946",
  },
  {
    id: 9,
    name: "Grey Overcoat",
    price: "4800 UAH",
    image: "https://m.media-amazon.com/images/I/71PgFLKowpL._AC_UY1000_.jpg",
  },
  {
    id: 10,
    name: "Black Tie",
    price: "450 UAH",
    image: "https://www.bows-n-ties.com/27212-xlarge_default/Solid-Black-Tie-in-Extra-Long-Length.jpg",
  },
  {
    id: 11,
    name: "White Sneakers",
    price: "2400 UAH",
    image: "https://martinvalen.com/29694-thickbox_default/men-s-casual-sneakers-iconic-white-black.jpg",
  },
  {
    id: 12,
    name: "Khaki Jacket",
    price: "3100 UAH",
    image: "https://media3.newlookassets.com/i/newlook/843499234/mens/mens-clothing/jackets-and-coats/khaki-twill-bomber-jacket.jpg?strip=true&qlt=50&w=720",
  },
];

export default function MenPage() {
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
          For Him
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
        {menItems.slice(0, page * itemsPerPage).map((item) => (
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