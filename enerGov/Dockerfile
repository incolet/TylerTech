FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
RUN rm /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/ener-gov/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d
EXPOSE 801
