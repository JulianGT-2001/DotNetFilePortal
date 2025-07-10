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