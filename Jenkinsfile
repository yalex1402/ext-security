pipeline{
    agent any
    environment {
        VERSION = '1.0.0'
    }
    stages{
        stage("build"){
            steps{
                echo "building the application version ${VERSION}..."
            }
        }
        stage("test"){
            steps{
                echo "testing the application..."
            }
        }
        stage("deploy"){
            steps{
                echo "deploying the application..."
            }
        }
    }
}