
FROM node:latest as build
EXPOSE 90
EXPOSE 443

WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

#STAGE 2
FROM nginx:latest 
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/market /usr/share/nginx/html