-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 18, 2020 at 10:46 AM
-- Server version: 10.1.28-MariaDB
-- PHP Version: 5.6.32

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `spk_bantar`
--

-- --------------------------------------------------------

--
-- Table structure for table `alternatif`
--

CREATE TABLE `alternatif` (
  `kode_alternatif` varchar(11) NOT NULL,
  `kode_kampung` varchar(11) NOT NULL,
  `nama_kampung` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `alternatif`
--

INSERT INTO `alternatif` (`kode_alternatif`, `kode_kampung`, `nama_kampung`) VALUES
('AL0001', 'KP0001', 'Eka Jaya'),
('AL0002', 'KP0002', 'Marene');

-- --------------------------------------------------------

--
-- Table structure for table `kampung`
--

CREATE TABLE `kampung` (
  `kode` varchar(12) NOT NULL,
  `nama` varchar(30) NOT NULL,
  `alamat` text NOT NULL,
  `kecamatan` varchar(50) NOT NULL,
  `kelurahan` varchar(50) NOT NULL,
  `rt` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `kampung`
--

INSERT INTO `kampung` (`kode`, `nama`, `alamat`, `kecamatan`, `kelurahan`, `rt`) VALUES
('KP0001', 'Eka Jaya', 'Eka jaya ', 'Paal Merah', 'Eka Jaya', '11'),
('KP0002', 'Marene', 'Marene', 'Marene', 'Marene', '12');

-- --------------------------------------------------------

--
-- Table structure for table `kategori_penilaian`
--

CREATE TABLE `kategori_penilaian` (
  `id` int(11) NOT NULL,
  `kategori_penilaian` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `kategori_penilaian`
--

INSERT INTO `kategori_penilaian` (`id`, `kategori_penilaian`) VALUES
(1, 'Kampung Bersih'),
(2, 'Kampung Aman'),
(3, 'Kampung Pintar');

-- --------------------------------------------------------

--
-- Table structure for table `kriteria`
--

CREATE TABLE `kriteria` (
  `id_kriteria` int(11) NOT NULL,
  `kode_penilaian` varchar(11) NOT NULL,
  `variabel_penilaian` text NOT NULL,
  `kriteria` varchar(50) NOT NULL,
  `bobot` int(11) NOT NULL,
  `relasi` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `kriteria`
--

INSERT INTO `kriteria` (`id_kriteria`, `kode_penilaian`, `variabel_penilaian`, `kriteria`, `bobot`, `relasi`) VALUES
(19, 'VP0001', 'sdfadsf', 'ada', 2, 'RL0001'),
(20, 'VP0001', 'sdfadsf', 'dfasdfaf', 1, 'RL0002'),
(21, 'VP0001', 'sdfadsf', 'da', 0, 'RL0003');

-- --------------------------------------------------------

--
-- Table structure for table `normalisasi`
--

CREATE TABLE `normalisasi` (
  `kode_normalisasi` varchar(11) NOT NULL,
  `tanggal` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `normalisasi_detail`
--

CREATE TABLE `normalisasi_detail` (
  `id` int(11) NOT NULL,
  `kode_normalisasi` varchar(11) NOT NULL,
  `kode_alternatif` varchar(11) NOT NULL,
  `id_kriteria` int(11) NOT NULL,
  `kriteria` text NOT NULL,
  `nilai_normalisasi` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `pengguna`
--

CREATE TABLE `pengguna` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(20) NOT NULL,
  `aktif` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pengguna`
--

INSERT INTO `pengguna` (`id`, `username`, `password`, `aktif`) VALUES
(1, 'admin', 'admin', 'aktif');

-- --------------------------------------------------------

--
-- Table structure for table `penilaian`
--

CREATE TABLE `penilaian` (
  `kode_penilaian` varchar(11) NOT NULL,
  `kategori` varchar(30) NOT NULL,
  `variabel_penilaian` text NOT NULL,
  `kepentingan` varchar(30) NOT NULL,
  `nilai_kepentingan` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `penilaian`
--

INSERT INTO `penilaian` (`kode_penilaian`, `kategori`, `variabel_penilaian`, `kepentingan`, `nilai_kepentingan`) VALUES
('VP0001', 'Kampung Bersih', 'Tersedia nya rumah layak huni', 'Sangat Tinggi', 1);

--
-- Triggers `penilaian`
--
DELIMITER $$
CREATE TRIGGER `cascade_delete` AFTER DELETE ON `penilaian` FOR EACH ROW delete from kriteria where kode_penilaian=kode_penilaian
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `perangkingan`
--

CREATE TABLE `perangkingan` (
  `kode_perangkingan` varchar(11) NOT NULL,
  `tanggal` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `perangkingan_detail`
--

CREATE TABLE `perangkingan_detail` (
  `id` int(11) NOT NULL,
  `kode_perangkingan` varchar(11) NOT NULL,
  `kode_alternatif` varchar(11) NOT NULL,
  `nilai_akhir` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `rating_kecocokan`
--

CREATE TABLE `rating_kecocokan` (
  `kode_rating_kecocokan` varchar(11) NOT NULL,
  `tanggal_rating` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `rating_kecocokan_detail`
--

CREATE TABLE `rating_kecocokan_detail` (
  `id` int(11) NOT NULL,
  `kode_rating_kecocokan` varchar(11) NOT NULL,
  `kode_alternatif` varchar(11) NOT NULL,
  `relasi_kriteria` varchar(11) NOT NULL,
  `kriteria` text NOT NULL,
  `bobot` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `alternatif`
--
ALTER TABLE `alternatif`
  ADD PRIMARY KEY (`kode_alternatif`);

--
-- Indexes for table `kampung`
--
ALTER TABLE `kampung`
  ADD PRIMARY KEY (`kode`);

--
-- Indexes for table `kategori_penilaian`
--
ALTER TABLE `kategori_penilaian`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kriteria`
--
ALTER TABLE `kriteria`
  ADD PRIMARY KEY (`id_kriteria`);

--
-- Indexes for table `normalisasi`
--
ALTER TABLE `normalisasi`
  ADD PRIMARY KEY (`kode_normalisasi`);

--
-- Indexes for table `normalisasi_detail`
--
ALTER TABLE `normalisasi_detail`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pengguna`
--
ALTER TABLE `pengguna`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `penilaian`
--
ALTER TABLE `penilaian`
  ADD PRIMARY KEY (`kode_penilaian`);

--
-- Indexes for table `perangkingan`
--
ALTER TABLE `perangkingan`
  ADD PRIMARY KEY (`kode_perangkingan`);

--
-- Indexes for table `perangkingan_detail`
--
ALTER TABLE `perangkingan_detail`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `rating_kecocokan`
--
ALTER TABLE `rating_kecocokan`
  ADD PRIMARY KEY (`kode_rating_kecocokan`);

--
-- Indexes for table `rating_kecocokan_detail`
--
ALTER TABLE `rating_kecocokan_detail`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `kategori_penilaian`
--
ALTER TABLE `kategori_penilaian`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `kriteria`
--
ALTER TABLE `kriteria`
  MODIFY `id_kriteria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `pengguna`
--
ALTER TABLE `pengguna`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `perangkingan_detail`
--
ALTER TABLE `perangkingan_detail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
