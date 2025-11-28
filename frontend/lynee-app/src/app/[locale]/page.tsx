'use client';

import React from 'react';
import {
  Box,
  Container,
  Typography,
  Button,
  Stack,
  Grid,
  IconButton
} from '@mui/material';
import {
  ArrowForward,
  FavoriteBorder,
  LocalMallOutlined,
} from '@mui/icons-material';
import Link from 'next/link';
import { useTranslations } from 'next-intl';
import { useParams } from 'next/navigation';
import BrandCarousel from '../../components/BrandCarousel';
// Helper component for Product Card
interface Product {
  id: string;
  name: string;
  brand: string;
  price: number;
  image: string;
  background?: string;
}
const ProductCard = ({ product }: { product: Product }) => (
  <Box sx={{ position: 'relative', backgroundColor: '#F5F5F5', height: '100%' }}>
    <Box
      sx={{
        position: 'relative',
        ...(product.background && {
          backgroundImage: `url(${product.background})`,
          backgroundSize: 'cover',
          backgroundPosition: 'center',
        }),
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        height: '90%', 
      }}
    >
      <Box
      component="img"
      src={product.image}
      alt={product.name}
      sx={{ width: '100%', height:'auto',  objectFit: 'contain',objectPosition: 'center',display: 'block',}}
    />
    </Box>
    
    <Box sx={{ position: 'absolute', top: 12, left: 12, backgroundColor: 'white', px: 1, py: 0.5 }}>
      <Typography variant="caption" sx={{ textTransform: 'uppercase', letterSpacing: 1 }}>
        {product.brand}
      </Typography>
    </Box>
    <Stack direction="row" spacing={1} sx={{ position: 'absolute', top: 8, right: 8 }}>
      <IconButton size="small" sx={{ backgroundColor: 'white', '&:hover': { backgroundColor: '#e0e0e0' } }}>
        <LocalMallOutlined fontSize="small" />
      </IconButton>
      <IconButton size="small" sx={{ backgroundColor: 'white', '&:hover': { backgroundColor: '#e0e0e0' } }}>
        <FavoriteBorder fontSize="small" />
      </IconButton>
    </Stack>
    <Box sx={{ p: 2, backgroundColor: 'white' }}>
      <Typography variant="body2" sx={{ textTransform: 'uppercase' }}>
        {product.name}
      </Typography>
      <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
        {product.price} UAH
      </Typography>
    </Box>
  </Box>
);
const HomePage: React.FC = () => {
  const t = useTranslations('HomePage');
  const { locale } = useParams();

  const newArrivals = [
    { id: '1', name: 'Cropped shirt', brand: 'Ralph Lauren', price: 980, image: '/img/newArrivals/1.png', background:'/img/background.png'},
    { id: '2', name: 'Cotton t-shirt', brand: 'Burberry', price: 12300, image: '/img/newArrivals/2.png', background:'/img/background.png' },
    { id: '3', name: 'Diamond tote s', brand: 'Jimmy Choo', price: 44730, image: '/img/newArrivals/3.png', background:'/img/background.png' },
    { id: '4', name: 'Zoey', brand: 'Jimmy Choo', price: 44730, image: '/img/newArrivals/4.png', background:'/img/background.png' },
  ];

  const otherCollections = [
    { id: '5', name: 'White dress', brand: 'ZARA', price: 988, image: '/img/otherColl/1.png' },
    { id: '6', name: 'Red overalls', brand: 'ZARA', price: 1099, image: '/img/otherColl/2.jpg' },
    { id: '7', name: 'Bermuda shorts', brand: 'MANGO', price: 2000, image: '/img/otherColl/3.png' },
    { id: '8', name: 'Draped denim dress', brand: 'Mohito', price: 2099, image: '/img/otherColl/4.png' },
  ];

  const brands = [
    { id: '1', name: 'Brand 1', image: '/img/brands/1.png' },
    { id: '2', name: 'Brand 2', image: '/img/brands/2.png' },
    { id: '3', name: 'Brand 3', image: '/img/brands/3.png' },
    { id: '4', name: 'Brand 4', image: '/img/brands/4.png' },
    { id: '5', name: 'Brand 5', image: '/img/brands/5.png' },
    { id: '6', name: 'Brand 6', image: '/img/brands/6.png' },
    { id: '7', name: 'Brand 7', image: '/img/brands/7.png' },
  ];

  return (
    <Box sx={{ backgroundColor: '#fff', overflowX:'hidden' }}>
      {/* Hero Section */}
      <Box
        sx={{
          height: { xs: '60vh', md: '90vh' },
          backgroundImage: 'url(/img/firstRhoteBagound.png)',
          backgroundSize: 'cover',
          backgroundPosition: 'center',
          backgroundRepeat: 'no-repeat',
          backgroundColor: '#f0f0f0',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'flex-start',
          color: 'white',
          position: 'relative',
          px: { xs: 2, md: 8 },
        }}
      >
        <Box>
          <Typography
            variant="h1"
            component="h1"
            sx={{
              fontWeight: 400,
              fontSize: { xs: '3rem', md: '6rem' },
              letterSpacing: '0.1em',
              textTransform: 'uppercase',
            }}
          >
            {t('hero.luxury')}
          </Typography>
          <Typography
            variant="h1"
            component="h1"
            sx={{
              fontWeight: 400,
              fontSize: { xs: '3rem', md: '6rem' },
              letterSpacing: '0.1em',
              textTransform: 'uppercase',
            }}
          >
            {t('hero.collection')}
          </Typography>
          <Typography variant="body1" sx={{ mt: 1, letterSpacing: '0.05em' }}>
            {t('hero.subtitle')}
          </Typography>
        </Box>
        <Stack direction="row" spacing={1} sx={{ position: 'absolute', bottom: 30, right: { xs: 16, md: 64 }, }}>
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', border: '1px solid white' }} />
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', backgroundColor: 'white' }} />
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', border: '1px solid white' }} />
        </Stack>
      </Box>

      {/* Shop by Category Section */}
      <Container maxWidth="xl" sx={{ py: { xs: 6, md: 10, textAlign: 'center', alignItems: 'center', justifyContent: 'center' } }}>
        <Grid container spacing={{ xs: 4, md: 8 }} alignItems="center">
          <Grid size={{ xs: 12, md: 5 }} sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', minHeight: { xs: 'auto', md: '600px' } }}>
            <Typography
              variant="h6"
              sx={{ mb: 1, fontWeight: 500, letterSpacing: '0.1em', textTransform: 'uppercase', fontSize: { xs: '1rem', md: '1.5rem' } }}
            >
              {t('shopByCategory')}
            </Typography>
            
            <Stack spacing={2}>
              {[t('categories.women'), t('categories.men'), t('categories.kids'), t('categories.accessories')].map((category) => (
                <Typography
                  key={category}
                  variant="h2"
                  component={Link}
                  href={`/${locale}/categories/${category.toLowerCase()}`}
                  sx={{
                    color: '#9E9E9E',
                    fontWeight: 300,
                    fontSize: { xs: '2.5rem', md: '4rem' },
                    letterSpacing: '0.05em',
                    textTransform: 'uppercase',
                    textDecoration: 'none',
                    transition: 'color 0.3s ease',
                    '&:hover': { color: 'text.primary' },
                  }}
                >
                  {category}
                </Typography>
              ))}
            </Stack>
        </Grid>
          <Grid size={{ xs: 12, md: 7 }}>
            <Box
              sx={{
                height: { xs: 400, md: 600 },
                backgroundImage: 'url(/img/imgNearCategories.png)',
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                borderRadius: 0,
                backgroundRepeat: 'no-repeat',
                backgroundColor: '#f0f0f0',
              }}
            />
           </Grid>
        </Grid>
      </Container>

      {/* Brands Section */}
      <Container maxWidth="xl" sx={{ py: 6 }}>
        <BrandCarousel brands={brands} />
      </Container>

      {/* New arrivals Section */}
      <Container maxWidth="lg" sx={{ py: { xs: 6, md: 10 } }}>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            mb: 4,
          }}
        >
          <Typography
            variant="h4"
            sx={{fontWeight: 300,}}
          >
            {t('newArrivals')}
          </Typography>
          <Button
            variant="text"
           endIcon={<Box sx={{
                width: 24, height: 24, borderRadius: '50%', border: '1px solid black',
                display: 'flex', alignItems: 'center', justifyContent: 'center'
            }}><ArrowForward sx={{ fontSize: 16 }} /></Box>}
            sx={{ color: 'text.primary', textTransform: 'none' }}
          >
            {t('viewAll')}
          </Button>
        </Box>

        <Grid container spacing={2}>
          {newArrivals.map((product) => (
            <Grid size={{ xs: 12, sm: 6, md: 3 }} key={product.id}>
              <ProductCard product={product} />
            </Grid>
          ))}
        </Grid>
      </Container>

      {/* Collection Gepur Section */}
      <Container maxWidth="xl" disableGutters sx={{ py: { xs: 6, md: 10 } }}>
        <Grid container>
            <Grid size={{ xs: 12, md: 6 }}>
                 <Box sx={{ position: 'relative', height: { xs: 400, md: 600 } }}>
                    <Box
                      component="img"
                      src="/img/CrystalRoseImg.png"
                      alt="Crystal Rose Collection"
                      sx={{
                        width: '100%',
                        height: '100%',
                        objectFit: 'cover',
                        display: 'block'
                      }}
                    />
                 </Box>
            </Grid>
            <Grid size={{ xs: 12, md: 6 }} sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', p: { xs: 4, md: 8 } }}>
                 <Typography variant="h6" sx={{ letterSpacing: '0.2em', textTransform: 'uppercase', fontSize: { xs: '0.8rem', md: '1rem' }, fontWeight: 400 }}>{t('crystalRose.title')}</Typography>
                 <Typography variant="h2" sx={{ fontWeight: 300, my: 2 }}>{t('crystalRose.collection')}</Typography>
                 <Typography variant="body1" color="text.secondary" sx={{ maxWidth: 500, mb: 10 }}>
                     {t('crystalRose.description')}
                 </Typography>
                 <Button variant="text" endIcon={<ArrowForward />} sx={{ color: 'text.primary', justifyContent: 'flex-start', p: 0 }}>
                     {t('crystalRose.button')}
                 </Button>
            </Grid>
        </Grid>
      </Container>

      {/* NUDE SUNSET Section */}
      <Box
        sx={{
          height: { xs: '50vh', md: '70vh' },
          backgroundImage: 'url(/img/NudeImg.png)',
          backgroundSize: 'cover',
          backgroundPosition: 'center',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
          color: 'white',
          textAlign: 'center',
        }}
      >
         </Box>

      {/* Explore other collections */}
      <Container maxWidth="xl" sx={{ py: { xs: 6, md: 10 }, backgroundColor: '#fff' }}>
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
          <Typography variant="h4" sx={{ fontWeight: 300 }}>
            {t('exploreCollections')}
          </Typography>
          <Button
            variant="text"
            endIcon={<Box sx={{
                width: 24, height: 24, borderRadius: '50%', border: '1px solid black',
                display: 'flex', alignItems: 'center', justifyContent: 'center'
            }}><ArrowForward sx={{ fontSize: 16 }} /></Box>}
            sx={{ color: 'text.primary', textTransform: 'none' }}
          >
            {t('viewAll')}
          </Button>
         </Box>
        <Grid container spacing={2}>
          {otherCollections.map((product) => (
            <Grid size={{ xs: 12, sm: 6, md: 3 }} key={product.id}>
              <ProductCard product={product} />
            </Grid>
          ))}
        </Grid>
         <Stack direction="row" spacing={1} sx={{ justifyContent: 'flex-end', mt: 4 }}>
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', backgroundColor: '#C4C4C4' }} />
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', backgroundColor: 'black' }} />
            <Box sx={{ width: 10, height: 10, borderRadius: '50%', backgroundColor: '#C4C4C4' }} />
        </Stack>
      </Container>

      {/* Summer Sale */}
      <Container maxWidth="xl" sx={{ py: { xs: 6, md: 10 }, textAlign: 'center', backgroundColor: '#fff' }}>
          <Typography variant="h1" sx={{ fontWeight: 400, fontSize: { xs: '3rem', md: '5rem' }, pb: 4 }}>{t('summerSale')}</Typography>
           <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
             <Box component="img" src="/img/SalesImg.png" sx={{ maxWidth: '100%', height: 'auto' }} />
           </Box>
      </Container>

    </Box>
  );
};

export default HomePage; 