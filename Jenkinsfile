pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/AlexShmulko/Lab2Pipeline.git' // Укажи свой репозиторий
            }
        }

        stage('Build and Push Docker Images') {
            steps {
                script {
                    sh 'docker build -t service1:latest -f Service1/Dockerfile .'
                    sh 'docker build -t service2:latest -f Service2/Dockerfile .'
                    sh 'docker build -t custom-nginx:latest -f Nginx/Dockerfile .'
                }
            }
        }

        stage('Deploy with Docker Compose') {
            steps {
                script {
                    sh 'docker-compose down'
                    sh 'docker-compose up -d --build'
                }
            }
        }

        stage('Post-deploy Verification') {
            steps {
                script {
                    sh 'docker ps' // Проверяем, что контейнеры работают
                    sh 'curl -f http://localhost/service1 || exit 1' // Проверяем Service1
                    sh 'curl -f http://localhost/service2 || exit 1' // Проверяем Service2
                }
            }
        }
    }
}
