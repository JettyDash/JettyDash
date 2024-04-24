# Use Node.js as base image
FROM node:alpine AS builder

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY client/package.json client/package-lock.json ./

# Install dependencies
RUN npm install --production

# Copy the rest of the application code
COPY ./client .

# Build the Next.js application
RUN npm run build

# Use Nginx as base image for the production environment
FROM nginx:alpine

# Set working directory to nginx config directory
WORKDIR /etc/nginx/conf.d

# Remove the default nginx configuration file
RUN rm default.conf

# Copy custom nginx configuration
COPY nginx.conf jettydash.conf

# Set the domain name
RUN sed -i 's/server_name _;/server_name jettydash.com;/g' jettydash.conf

# Set working directory to app directory
WORKDIR /usr/share/nginx/html

# Copy the built Next.js application from the builder stage to nginx html directory
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public

# Expose port 80
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
