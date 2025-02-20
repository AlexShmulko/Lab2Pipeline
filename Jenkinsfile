pipeline {
    agent any

    environment {
        REPO_URL = 'https://github.com/AlexShmulko/Lab2Pipeline.git'
        BRANCH_NAME = 'LabBranch'
        CREDENTIALS_ID = 'your-credentials-id' // Если репозиторий приватный
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: "${BRANCH_NAME}", credentialsId: "${CREDENTIALS_ID}", url: "${REPO_URL}"
            }
        }

        stage('Build and Push Docker Images') {
            steps {
                script {
                    sh 'docker build -t service1-image ./Service1'
                    sh 'docker build -t service2-image ./Service2'
                    sh 'docker pull nginx:latest'
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
                    sh 'sleep 10'
                    sh 'curl -f http://localhost/service1/ || exit 1'
                    sh 'curl -f http://localhost/service2/ || exit 1'
                }
            }
        }
    }

    post {
        failure {
            echo 'Build or deployment failed!'
        }
        success {
            echo 'Deployment successful!'
        }
    }
}
