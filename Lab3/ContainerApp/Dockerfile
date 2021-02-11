FROM node:carbon 

WORKDIR /usr/src/app 

COPY package*.json ./ 

COPY index2.js ./ 

RUN npm install 

ENTRYPOINT [ "npm","start" ]