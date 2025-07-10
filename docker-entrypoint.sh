#!/usr/bin/env bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE TABLE IF NOT EXISTS tb_file (
        id UUID NOT NULL,
        original_name TEXT NOT NULL,
        path TEXT NOT NULL,
        size_in_bytes BIGINT NOT NULL,
        mime_type TEXT NOT NULL,
        uploaded_by TEXT NOT NULL,
        uploaded_at TIMESTAMP NOT NULL,
        CONSTRAINT tbf_id_pk
        PRIMARY KEY (id)
    );
EOSQL

exec "$@"