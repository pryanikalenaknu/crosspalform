const axios = require('axios');
const https = require('https');
const logger = require('../config/logger');

const API_BASE_URL = 'http://localhost:6674/api';

const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
    httpsAgent: new https.Agent({
        rejectUnauthorized: false,
    }),
});

describe('Endpoints tests', () => {
    beforeAll(() => {
        logger.info('Starting References API tests');
    });

    afterAll(() => {
        logger.info('Finished References API tests');
    });

    test('GET /api/RefPaymentMethods should return all RefPaymentMethod', async () => {
        try {
            logger.info('Testing GET /api/RefPaymentMethods');
            const response = await axiosInstance.get('/RefPaymentMethods');

            logger.info(`Received ${JSON.stringify(response.data)} RefPaymentMethods`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing GET /api/RefPaymentMethods: ${error.message}`);
            throw error;
        }
    });

    test('GET /api/RefOrderStatusCodes should return all RefOrderStatusCodes', async () => {
        try {
            logger.info('Testing GET /api/RefOrderStatusCodes');
            const response = await axiosInstance.get('/RefOrderStatusCodes');

            logger.info(`Received ${JSON.stringify(response.data)} RefOrderStatusCodes`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing GET /api/RefOrderStatusCodes: ${error.message}`);
            throw error;
        }
    });

    test('GET /api/CustomerOrders should return all CustomerOrders', async () => {
        try {
            logger.info('Testing GET /api/CustomerOrders');
            const response = await axiosInstance.get('/CustomerOrders');

            logger.info(`Received ${JSON.stringify(response.data)} CustomerOrders`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing GET /api/CustomerOrders: ${error.message}`);
            throw error;
        }
    });

    test('POST /api/Search/search', async () => {
        const productsIds = 1;
        try {
            logger.info(`Testing POST /api/Search/search`);
            const response = await axiosInstance.post(`/Search/search`, {
                params: { productsIds }
            });

            logger.info(`Received accounts: ${JSON.stringify(response.data)}`);
            expect(response.status).toBe(200);
            expect(Array.isArray(response.data.$values)).toBe(true);
        } catch (error) {
            logger.error(`Error testing POST /api/Search/search: ${error.message}`);
            throw error;
        }
    });
});


describe('Database Integration Tests', () => {

    describe('SQLLite Server', () => {
        test('SQLLite Server Tests', async () => {
            try {
                logger.info('Testing SQL Server connection');
                const response = await axiosInstance.get('/Department');
                logger.info('Successfully connected to SQLLite Server');
                expect(response.status).toBe(200);
            } catch (error) {
                logger.error(`SQLLite Server test failed: ${error.message}`);
                throw error;
            }
        });
    });
});