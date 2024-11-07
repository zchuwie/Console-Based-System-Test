-- MySQL dump 10.13  Distrib 8.0.35, for Win64 (x86_64)
--
-- Host: localhost    Database: finals
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `drug_inventory`
--

DROP TABLE IF EXISTS `drug_inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drug_inventory` (
  `drug_id` int NOT NULL AUTO_INCREMENT,
  `drug_name` varchar(255) DEFAULT NULL,
  `expiration_date` date DEFAULT NULL,
  `manufacturer` varchar(255) DEFAULT NULL,
  `price` decimal(5,2) DEFAULT NULL,
  `prescription_needed` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`drug_id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drug_inventory`
--

LOCK TABLES `drug_inventory` WRITE;
/*!40000 ALTER TABLE `drug_inventory` DISABLE KEYS */;
INSERT INTO `drug_inventory` VALUES (1,'Lidocaine, Menthol','2025-05-29','Photolist',366.01,1),(2,'Ribavirin','2024-02-20','Dynazzy',220.52,1),(3,'LISINOPRIL','2025-10-13','Izio',676.02,1),(4,'Phentermine Hydrochloride','2024-10-28','Oyope',96.04,1),(5,'Losartan Potassium','2025-02-01','Youtags',593.50,1),(6,'OXYBENZONE, OCTISALATE, AVOBENZONE','2024-11-30','Mybuzz',327.43,0),(7,'OCTINOXATE, TITANIUM DIOXIDE, and ZINC OXIDE','2024-08-24','Linkbuzz',272.96,1),(8,'Triamcinolone Acetonide','2025-12-11','Twinte',312.05,0),(9,'Omeprazole','2024-05-22','Wordpedia',447.50,0),(10,'penciclovir','2024-08-05','Blognation',437.65,0),(11,'Oxcarbazepine','2025-11-25','Quinu',371.62,0),(12,'docusate calcium','2025-06-13','Devshare',322.45,1),(13,'ACETAMINOPHEN','2025-03-05','Yombu',917.32,0),(14,'milnacipran hydrochloride','2024-05-10','Camido',173.18,1),(15,'AVOBENZONE, HOMOSALATE, OCTINOXATE, OCTISALATE, OXYBENZONE','2025-11-15','Kwilith',470.71,1),(16,'HELIUM','2024-11-23','Zazio',486.40,1),(17,'ARMODAFINIL','2025-07-06','Layo',993.72,1),(18,'tolnaftate','2025-04-15','Browsetype',602.60,1),(19,'ZINC OXIDE','2024-10-12','Fanoodle',525.29,1),(20,'Methotrexate','2024-02-09','Muxo',487.45,1),(21,'Matricaria recutita, Mercurius Solubilis, Anemone patens and Sulphur','2024-06-17','Kwinu',33.38,1),(22,'Titanium Dioxide and Zinc Oxide','2024-02-05','Browsecat',191.59,0),(23,'Triclosan','2025-11-13','Oyonder',949.30,0),(24,'Cantaloupe Cucumis melo','2024-07-20','Gabspot',849.56,0),(25,'Enalapril Maleate','2025-07-02','Zoonder',618.44,1),(26,'METHYL UNDECENOYL LEUCINATE','2024-07-07','Dablist',31.57,0),(27,'TITANIUM DIOXIDE','2025-04-07','Jabbertype',876.13,1),(28,'Acetaminophen, Dextromethorphan Hydrobromide, Phenylephrine Hydrochloride and Doxylamine succinate','2024-05-23','Yoveo',137.62,0),(29,'Homeopathic Liquid','2024-08-08','Yoveo',851.35,1),(30,'Valsartan and Hydrochlorothiazide','2025-08-11','Edgeclub',33.45,0),(31,'telmisartan and hydrochlorothiazide','2025-10-28','Muxo',202.59,0),(32,'Granisetron Hydrochloride','2024-10-29','Realcube',92.35,1),(33,'Diclofenac Epolamine','2024-07-29','Vipe',35.76,0),(34,'Amlodipine Besylate and Benazepril HCL','2024-05-11','JumpXS',244.98,0),(35,'Black Oak','2025-09-06','Rhyloo',1.15,1),(36,'Carvedilol','2024-04-21','Tavu',625.72,0),(37,'Ethyl Alcohol','2024-07-15','Wordpedia',85.68,0),(38,'ACETYLCYSTEINE','2025-08-22','Shuffletag',340.09,1),(39,'Norethindrone','2025-10-25','Voonte',874.21,0),(40,'Ofloxacin','2025-01-02','Rooxo',287.50,1),(41,'Mineral oil, Petrolatum, Phenylephrine HCl, Shark liver oil','2024-07-11','Dynabox',397.15,1),(42,'HYDROCODONE BITARTRATE AND ACETAMINOPHEN','2025-08-28','Skivee',666.42,0),(43,'ARIPIPRAZOLE','2025-02-27','Eire',194.30,0),(44,'Carya ovata','2024-09-04','Devify',524.50,1),(45,'KETOROLAC TROMETHAMINE','2024-05-02','Twiyo',707.62,0),(46,'TITANIUM DIOXIDE','2024-10-25','Dabshots',986.96,1),(47,'TITANIUM DIOXIDE','2024-01-27','Wikivu',685.07,1),(48,'Lisinopril','2025-01-31','Chatterbridge',33.48,0),(49,'SULFACETAMIDE SODIUM, SULFUR','2025-05-09','Dabfeed',642.82,1),(50,'Benazepril Hydrochloride','2024-02-12','Dazzlesphere',90.38,0);
/*!40000 ALTER TABLE `drug_inventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hashedpassword`
--

DROP TABLE IF EXISTS `hashedpassword`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hashedpassword` (
  `hash_id` int NOT NULL AUTO_INCREMENT,
  `hashSalt` varchar(255) DEFAULT NULL,
  `hashPassword` varchar(255) DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`hash_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hashedpassword`
--

LOCK TABLES `hashedpassword` WRITE;
/*!40000 ALTER TABLE `hashedpassword` DISABLE KEYS */;
INSERT INTO `hashedpassword` VALUES (1,'94466eb9305cbb1792a8bad57c46d040e2297763e92d6394082da2bfd980a745','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',1),(2,'6a5fc6fdeb4955351f13dc390df43426d5226db2477a4f313f0a9b7789dc944c','372d48bef2a83f98b20fdcf9f8fad00532fb0bc6ad22fa6de0f11ea9b900d047',2),(5,'038bac791d5e8a80edebfe68d671c4fa39b878c55b507e3292fa70eb7914e4a5','9f96f382ffec42d1cfc6fe5bf4faeffa9bcf820a3a882998cee9ac8182edc0bf',7);
/*!40000 ALTER TABLE `hashedpassword` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temporarycartuser`
--

DROP TABLE IF EXISTS `temporarycartuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `temporarycartuser` (
  `tempCart_id` int NOT NULL AUTO_INCREMENT,
  `drug_id` int DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  `alreadyCheckout` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`tempCart_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temporarycartuser`
--

LOCK TABLES `temporarycartuser` WRITE;
/*!40000 ALTER TABLE `temporarycartuser` DISABLE KEYS */;
INSERT INTO `temporarycartuser` VALUES (1,1,2,1),(2,10,2,1),(3,21,2,1),(4,32,2,1),(5,50,2,1),(6,50,2,1),(7,48,2,1),(8,21,2,1),(9,31,2,1),(10,21,2,1),(11,49,2,1),(12,49,2,1);
/*!40000 ALTER TABLE `temporarycartuser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `useraccount`
--

DROP TABLE IF EXISTS `useraccount`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `useraccount` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `useraccount`
--

LOCK TABLES `useraccount` WRITE;
/*!40000 ALTER TABLE `useraccount` DISABLE KEYS */;
INSERT INTO `useraccount` VALUES (1,'admin','admin@gmail.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a994466eb9305cbb1792a8bad57c46d040e2297763e92d6394082da2bfd980a745'),(2,'denz','denz@gmail.com','372d48bef2a83f98b20fdcf9f8fad00532fb0bc6ad22fa6de0f11ea9b900d0476a5fc6fdeb4955351f13dc390df43426d5226db2477a4f313f0a9b7789dc944c'),(7,'alan','alan@gmail.com','0b2b7111583a191ba9c3f57fb7a003434008d7d67a21846cde0cd54040b456543541868612fd3a093cfc9a29852d03dea9dc4927f1063b59a43f0c9faa63f5e2');
/*!40000 ALTER TABLE `useraccount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usercarttransaction`
--

DROP TABLE IF EXISTS `usercarttransaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usercarttransaction` (
  `userCart_id` int NOT NULL AUTO_INCREMENT,
  `drug_id` int DEFAULT NULL,
  `transaction_id` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`userCart_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usercarttransaction`
--

LOCK TABLES `usercarttransaction` WRITE;
/*!40000 ALTER TABLE `usercarttransaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `usercarttransaction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertransaction`
--

DROP TABLE IF EXISTS `usertransaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usertransaction` (
  `userTransactionID` int NOT NULL AUTO_INCREMENT,
  `transaction_id` varchar(255) DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`userTransactionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertransaction`
--

LOCK TABLES `usertransaction` WRITE;
/*!40000 ALTER TABLE `usertransaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `usertransaction` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-07 21:09:40
