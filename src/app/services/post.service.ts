import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from '../models/auth.models';
import { PostDto, CommentDto, LikesDto } from '../models/post.models';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7001/api/post'; // Gateway API URL
  private commentApiUrl = 'https://localhost:7001/api/comment'; // Gateway API URL
  private likesApiUrl = 'https://localhost:7001/api/likes'; // Gateway API URL

  getPosts(): Observable<ApiResult<PostDto[]>> {
    return this.http.get<ApiResult<PostDto[]>>(`${this.apiUrl}/GetPosts`);
  }

  getPost(id: string): Observable<ApiResult<PostDto>> {
    return this.http.get<ApiResult<PostDto>>(`${this.apiUrl}/GetPost/${id}`);
  }

  removePost(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/RemovePost/${id}`);
  }

  createPost(content: string): Observable<ApiResult<PostDto>> {
    return this.http.post<ApiResult<PostDto>>(`${this.apiUrl}/CreatePost`, {
      content: content
    });
  }

  createComment(postId: string, text: string): Observable<ApiResult<CommentDto>> {
    return this.http.post<ApiResult<CommentDto>>(`${this.commentApiUrl}/CreateComment`, {
      postId: postId,
      text: text
    });
  }

  removeComment(postId: string, commentId: string): Observable<ApiResult<any>> {
    return this.http.request<ApiResult<any>>('DELETE', `${this.commentApiUrl}/RemoveComment/${commentId}`, {
      body: {
        postId: postId,
        commentId: commentId
      }
    });
  }

  createLike(postId: string, userId: string): Observable<ApiResult<LikesDto>> {
    return this.http.post<ApiResult<LikesDto>>(`${this.likesApiUrl}/CreateLike`, {
      postId: postId,
      userId: userId
    });
  }

  removeLike(postId: string, likeId: string): Observable<ApiResult<any>> {
    return this.http.request<ApiResult<any>>('DELETE', `${this.likesApiUrl}/RemoveLike/${likeId}`, {
      body: {
        postId: postId,
        likeId: likeId
      }
    });
  }
}

