using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;

public class RtpPacket
	{
		
        private uint rtp_length; /* fixed RTP packet header size */
        private uint version;	/* version de RTP */ 
        private bool padding; /* relleno  **/
        private bool extension; /* extension  */
        private uint csrc_count; /* the source count */
        private bool marker; /* the marker status */
        private uint payload_type;    /* the type of payload  */
        private static ushort sequence_no;  /* nro de secuencia */
        private uint timestamp;  /* timestamp */
        private uint source_id;  /* the source id  of the RTP packet */
	    private byte[] csrc_id; 
	 	private byte[] extension_header;  
		private byte[] data;  
        
        public RtpPacket(){  /* carga por defecto */
             rtp_length = 12; 
             padding = false; 
             extension = false; 
             csrc_count = 0; 
             marker = true; 
             payload_type = 26; 
			 version = 2;          
        } 
	
	 	public RtpPacket(byte[] packet){ 
			rtp_length = 12;
			decode(packet);          
        } 

        public RtpPacket(int length, int v, bool p, bool e, int crsc, bool m, int payl){    
			rtp_length = (uint)length; 
             version = (uint)v;	
             padding = p; 
             extension = e; 
             csrc_count = (uint)crsc; 
             marker = m; 
             payload_type = (uint)payl;    
         
        }
	/************************ GET'S ****************************/
	
        public byte[] getData(){ return data; }
			
		public uint getTimestamp(){ return timestamp; }
			
		public ushort getSequenceNo(){ return sequence_no;}
			
		public byte[] get_csrc_id(){ return csrc_id; }
	
		public byte[] get_extension_header(){ return extension_header; }
		
		public uint get_source_id(){ return source_id; }
	
		public uint getRtpLength(){ return rtp_length; }
	
		public uint getVersion(){ return version; }
	
		public bool getPadding(){ return padding; }
	
		public bool getExtension(){ return extension; }
	
		public uint getCsrc(){ return csrc_count; }
	
		public bool getMarker(){ return marker; }
	
		public uint getPayload(){ return payload_type; }
	
	/************************ SET'S ****************************/
	
		public void set_csrc_id(byte[] c){
			csrc_id = new byte[c.Length];
			csrc_id = c;
		}
		
		public void set_extension_header(byte[] eh){
			extension_header = new byte[eh.Length];
			extension_header = eh;
		}
	
		public void set_source_id(uint s){
			source_id = s;
		}
	
		public void setData(byte[] d){		
			data = new byte[d.Length];
			data = d;
		}
			
		public void setTimestamp(uint ts){
			timestamp = ts;
		}
			
		public void setSequenceNo(ushort sn){
			sequence_no = sn;
		}
			
		public void setRtpLength(int rtpL){
			rtp_length = (uint)rtpL;
		}
			
		public void setVersion(int ver){
			version = (uint)ver;
		}
	
	    public void setPadding(bool pad){
			padding = pad;
		}
	
	    public void setExtension(bool ext){
			extension = ext;
		}
	
	    public void setCsrc(int csrc){
			csrc_count = (uint)csrc;
		}
	
	    public void setMarker(bool mark){
			marker = mark;
		}
	
	    public void setPayload(int pay){
			payload_type = (uint)pay;
		}
		

       public static byte[] ReverseBytes(byte[] inArray)
   {
      byte temp;
      int highCtr = inArray.Length - 1;

      for (int ctr = 0; ctr < inArray.Length / 2; ctr++)
      {
         temp = inArray[ctr];
         inArray[ctr] = inArray[highCtr];
         inArray[highCtr] = temp;
         highCtr -= 1;
      }
      return inArray;
   }
	
	
	/************************ DECODE ****************************/
	
        public byte[] decode(byte[] datos){  
			
			byte primero = datos[0];
			
			// CONVIERTO A BIT EL BYTE 0
			string bits = Convert.ToString(primero, 2).PadRight(8, '0');
		
			version =  (uint)Convert.ToInt32(bits.Substring(0,2));//Debug.Log("version "+version);//los 2 primeros bits
            padding =  false;
			if (bits.Substring(2,1)== "1")
				padding = true;
            
		    extension = false;
		    if (bits.Substring(3,1)== "1")
				extension = true;
            csrc_count = (uint)Convert.ToInt32(bits.Substring(4,4));
		
			byte segundo = datos[1];
			// CONVIERTO A BIT EL BYTE 1
			string bits2 = Convert.ToString(segundo, 2).PadRight(8, '0');
				
            marker = false;
		    if (bits2.Substring(0,1) == "1")
				marker = false;
			
            payload_type = (uint)Convert.ToInt32(bits2.Substring(1,7));
           
			sequence_no = System.BitConverter.ToUInt16(datos, 2);
            
			timestamp = BitConverter.ToUInt32( datos, 4 );
		
			//Debug.Log("timestamp "+timestamp);
			source_id = System.BitConverter.ToUInt32(datos, 12);
			
			if (extension)
				extension_header = new byte[csrc_count*2];
				for(int i=0; i<csrc_count*2; i++){
					extension_header[i]=datos[16+i];
				}
		
			data = new byte[datos.Length-rtp_length];
			for(int i=0; i< datos.Length-rtp_length; i++ )
			 	data[i] = datos[rtp_length+i];
            
			return data;
        }
	
		
	/************************ ENCODE ****************************/
	
		public byte[] encode(){
		
		   int tamanio = (int)(rtp_length + csrc_count*2 + data.Length);
		   byte[] data_result = new byte[tamanio];
		//version
           string v = Convert.ToString(version,2).PadRight(2, '0');
		//padding
		   string p = "0";
		   if (padding) 
			p = "1";
		//extension
		   string e = "0";
		   if (extension) 
			e = "1";
		//cc_
			string cc = Convert.ToString(csrc_count,2).PadRight(4, '0');
		
			string data0 = String.Concat(v,p,e,cc);
		  
		    byte cero = Convert.ToByte(data0,2);
		
		//marker			
		   string m = "0";
		   if (marker) 
			m = "1";
		//payload
			string payl = Convert.ToString(payload_type,2).PadLeft(7, '0');
		
			string data1 = String.Concat(m,payl);
					
			byte uno = Convert.ToByte(data1,2);
		//nro_sequence
			byte[] nro_seq = BitConverter.GetBytes(sequence_no);
		   
		//timestamp

		/*	
		 * int j=1;
		  Debug.Log("time en encode:" +timestamp/Math.Pow(2,8*j));
			double aux = timestamp/Math.Pow(2,8*j);
			
			while(aux!=0){
			j++;
			aux = timestamp/Math.Pow(2,8*j);
			}
			Debug.Log("bytes utilizados "+j);
			*/
		
			byte[] t = BitConverter.GetBytes(timestamp);
		
			/*for(int i =0; i<t.Length; i++)
			Debug.Log("t["+i+"]" +t[i]);*/
		
		//source_id
			byte[] ssrc = BitConverter.GetBytes(source_id);
		
			int pos = 2;
			data_result[0] = cero;
			data_result[1]= uno;
		//se copia l nro de scuencia
		
		Array.Copy(nro_seq,0,data_result,pos,2);
		pos = pos + 2;
		Array.Copy(t,0,data_result,pos,4);
		//se copia el timstamp
	    pos = pos +4;
		Array.Copy(ssrc,0,data_result,pos,4);
		//se copia el ssrc
		pos = pos + 4;
		//if extension==true copio el encabezado 
			if (extension){
				Array.Copy(csrc_id,0,data_result,pos,csrc_id.Length);
				pos = pos + csrc_id.Length;
				Array.Copy(extension_header,0,data_result,pos,extension_header.Length);
				pos = pos + extension_header.Length;
				/*for(int i=0; i< csrc_id.Length; i++){
					data_result[pos+i] = csrc_id[i];
					pos++;
				}
			
				for(int i=0; i< extension_header.Length; i++){
					data_result[pos+i] = extension_header[i];
					pos++;
				}*/
				
			}
		
		//copio datos
		 Array.Copy(data,0,data_result,pos,data.Length);
		
	/*	for(int i =4; i<8; i++){
			
						Debug.Log("data-result["+i+"]" +data_result[i]);

		}*/
		   return data_result;
			
		}
    }
	


