import psycopg2
import json
from flask import (
    Flask,
    request,
    jsonify
)

app = Flask(__name__)

DB_CONFIG = {
    "host": "db",
    "port": "5432",
    "database": "python.service",
    "user": "postgres",
    "password": "admin"
}

@app.route('/api/refresh', methods=['POST'])
def refresh():
    '''
    Endpoint - refresh
    '''
    try:
        user = request.json 

        processed_user = {
            **user,
            "processed": True
        }

        conn = psycopg2.connect(**DB_CONFIG)
        cursor = conn.cursor()

        cursor.execute(
            "INSERT INTO processed_users (original_data, processed_data) VALUES (%s, %s)",
            (json.dumps(user), json.dumps(processed_user))
        )

        conn.commit()
        cursor.close()
        conn.close()

        return jsonify({
            "status": "success",
            "processed_user": processed_user
        })

    except Exception as e:
        return jsonify(
        {
            "status": "error", 
            "message": str(e)
        }), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
