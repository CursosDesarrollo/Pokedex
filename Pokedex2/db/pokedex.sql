-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3307
-- Tiempo de generación: 26-12-2022 a las 16:48:58
-- Versión del servidor: 10.4.17-MariaDB
-- Versión de PHP: 8.0.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `pokedex`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_pokemon`
--

CREATE TABLE `detalle_pokemon` (
  `ID_DETALLE_POKEMON` int(11) NOT NULL,
  `ID_POKEMON` int(11) DEFAULT NULL,
  `ID_TIPO_POKEMON` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pokemon`
--

CREATE TABLE `pokemon` (
  `ID_POKEMON` int(11) NOT NULL,
  `NOMBRE` varchar(100) NOT NULL,
  `IMAGEN_URL` varchar(300) NOT NULL,
  `FECHA_CREACION` timestamp NOT NULL DEFAULT current_timestamp(),
  `FECHA_ACTUALIZACION` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_pokemon`
--

CREATE TABLE `tipo_pokemon` (
  `ID_TIPO_POKEMON` int(11) NOT NULL,
  `NOMBRE` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipo_pokemon`
--

INSERT INTO `tipo_pokemon` (`ID_TIPO_POKEMON`, `NOMBRE`) VALUES
(1, 'bicho'),
(2, 'dragon'),
(3, 'hada'),
(4, 'fuego'),
(5, 'fantasma'),
(6, 'tierra'),
(7, 'normal'),
(8, 'psiquico'),
(9, 'acero'),
(10, 'siniestro'),
(11, 'electrico'),
(12, 'lucha'),
(13, 'volador'),
(14, 'planta'),
(15, 'hielo'),
(16, 'veneno'),
(17, 'roca'),
(18, 'agua');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `detalle_pokemon`
--
ALTER TABLE `detalle_pokemon`
  ADD PRIMARY KEY (`ID_DETALLE_POKEMON`),
  ADD KEY `FK_DETALLE_TIPO` (`ID_TIPO_POKEMON`),
  ADD KEY `FK_POKEMON_DETALLE` (`ID_POKEMON`);

--
-- Indices de la tabla `pokemon`
--
ALTER TABLE `pokemon`
  ADD PRIMARY KEY (`ID_POKEMON`);

--
-- Indices de la tabla `tipo_pokemon`
--
ALTER TABLE `tipo_pokemon`
  ADD PRIMARY KEY (`ID_TIPO_POKEMON`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `detalle_pokemon`
--
ALTER TABLE `detalle_pokemon`
  MODIFY `ID_DETALLE_POKEMON` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `pokemon`
--
ALTER TABLE `pokemon`
  MODIFY `ID_POKEMON` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `tipo_pokemon`
--
ALTER TABLE `tipo_pokemon`
  MODIFY `ID_TIPO_POKEMON` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detalle_pokemon`
--
ALTER TABLE `detalle_pokemon`
  ADD CONSTRAINT `FK_DETALLE_TIPO` FOREIGN KEY (`ID_TIPO_POKEMON`) REFERENCES `tipo_pokemon` (`ID_TIPO_POKEMON`),
  ADD CONSTRAINT `FK_POKEMON_DETALLE` FOREIGN KEY (`ID_POKEMON`) REFERENCES `pokemon` (`ID_POKEMON`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
